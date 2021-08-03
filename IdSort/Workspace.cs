using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using IdSort.Properties;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using TagLib;
using File = System.IO.File;

namespace IdSort
{
    public class Workspace
    {
        public const int FormatVersion = 1;

        public static JsonSerializerSettings JsonSettings = new()
        {
            Formatting = Formatting.None,
            TypeNameHandling = TypeNameHandling.Objects
        };

        public delegate void ReportProgress(string currentFile, int completed, int outOf);

        public string RootPath;

        public List<GenericFile> Files = new();

        /// <summary>
        /// List of possible music/audio file extensions.<br />
        /// Any file with one of these extensions will attempted to be indexed and tags read.
        /// </summary>
        public List<string> PotentialFileExtensions = new () {"3gp", "aa", "aac", "aax", "act", "aiff", "alac", "amr", "ape", "au", "awb", "dss", "dvf", "flac", "gsm", "iklax", "ivs", "m4a", "m4b", "m4p", "mmf", "mp3", "mpc", "msv", "nmf", "ogg", "oga", "mogg", "opus", "ra", "rm", "rf64", "sln", "tta", "voc", "vox", "wav", "wma", "wv", "webm", "8svx", "cda"};
        
        [JsonIgnore]
        public int TotalFiles { get; private set; }
        [JsonIgnore]
        public int TotalMusicFiles { get; private set; }
        [JsonIgnore]
        public int TotalCorruptedFiles { get; private set; }

        public int CountPotentialIndexableFiles(string path)
        {
            path = Path.GetFullPath(path);

            int count = 0;
            foreach (string file in Directory.GetFiles(path))
            {
                if (PotentialFileExtensions.Any(ext => file.EndsWith("." + ext, StringComparison.InvariantCultureIgnoreCase)))
                    count++;
            }

            foreach (string dir in Directory.GetDirectories(path))
                count += CountPotentialIndexableFiles(dir);

            return count;
        }

        public void Index(string path, CancellationToken cancel, ReportProgress report)
        {
            path = Path.GetFullPath(path);
            RootPath = path;

            int total = CountPotentialIndexableFiles(path);
            int completed = 0;

            IndexDirectoryInternal(path, cancel, file =>
            {
                report.Invoke(file, ++completed, total);
            });

            CalculateStatistics();
        }

        private void IndexDirectoryInternal(string path, CancellationToken cancel, Action<string> fileIndexed = null)
        {
            // Index files
            foreach (string file in Directory.GetFiles(path))
            {
                // Try to read tags
                TagLib.File format = null;
                try
                {
                    format = TagLib.File.Create(file, ReadStyle.None);
                }
                catch (UnsupportedFormatException)
                {
                    continue; // skip this file
                }
                catch (CorruptFileException)
                {
                    Files.Add(new AudioFile(file, null)
                    {
                        CorruptReason = "Unreadable"
                    });
                    continue;
                }

                //if (format.Properties is { AudioBitrate: > 0 })
                if (format.TagTypes.HasFlag(TagTypes.Xiph) ||
                    format.TagTypes.HasFlag(TagTypes.FlacMetadata) ||
                    format.TagTypes.HasFlag(TagTypes.Id3v1) ||
                    format.TagTypes.HasFlag(TagTypes.Id3v2) ||
                    format.TagTypes.HasFlag(TagTypes.Ape) ||
                    format.TagTypes.HasFlag(TagTypes.Apple))
                {
                    Files.Add(new AudioFile(file, Tags.FromTagLibSharp(format.Tag))
                    {
                        CorruptReason = format.PossiblyCorrupt ? string.Join(", ", format.CorruptionReasons) : null,
                        TagLevel = format.Tag.GetTagLevel()
                    });

                    fileIndexed?.Invoke(file);
                }
                else
                {
                    Files.Add(new GenericFile(file));
                }

                if (cancel.IsCancellationRequested) return;
            }

            // Index subdirectories
            foreach (string subDirectory in Directory.GetDirectories(path))
            {
                IndexDirectoryInternal(subDirectory, cancel, fileIndexed);
                if (cancel.IsCancellationRequested) return;
            }
        }

        public void CalculateStatistics()
        {
            TotalFiles = 0;
            TotalMusicFiles = 0;
            TotalCorruptedFiles = 0;

            foreach (GenericFile file in Files)
            {
                if (file is AudioFile audioFile)
                {
                    TotalMusicFiles++;

                    if (audioFile.IsCorrupted)
                        TotalCorruptedFiles++;
                }

                TotalFiles++;
            }
        }

        /// <summary>
        /// Apply staged changes in internal state (this doesn't apply anything to disk).
        /// </summary>
        public void ApplyStagedChanges()
        {
            foreach (var file in Files)
            {
                file.Path = file.StagingOrRealPath;
                file.StagingPath = null;
            }
        }

        /// <summary>
        /// Constructs a tree view based on this workspace.
        /// </summary>
        /// <param name="tree">Tree view to populate.</param>
        /// <param name="showStaging">Show staging file structure?</param>
        public void BuildTreeView(TreeView tree, bool showStaging = false)
        {
            // Create workspace root node
            var workspaceNode = new ObjectSelectorEditor.SelectorNode($"Workspace", new WorkspaceNode(this, tree))
            {
                ForeColor = Color.Navy
            };

            // Split paths - this allows us to build a tree of directories easily
            var splitPaths = new List<string[]>();
            foreach (GenericFile file in Files)
            {
                splitPaths.Add(Path.GetRelativePath(RootPath, showStaging ? file.StagingOrRealPath : file.Path)
                    .Split(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar));
            }

            // Build directory tree
            var directories = new Dictionary<string, TreeNode>
            {
                {"", workspaceNode}
            };
            foreach (string[] splitPath in splitPaths)
            {
                string pieced = "";
                foreach (string part in splitPath.SkipLast(1))
                {
                    TreeNode parent = directories[pieced];

                    pieced = (pieced + Path.DirectorySeparatorChar + part)
                        .Trim(Path.DirectorySeparatorChar, System.IO.Path.AltDirectorySeparatorChar);

                    if (directories.ContainsKey(pieced))
                        continue;
                    
                    var node = new ObjectSelectorEditor.SelectorNode(part,
                        new WorkspaceDirectory(Path.GetFullPath(RootPath + Path.DirectorySeparatorChar + pieced)));
                    node.ImageKey = node.SelectedImageKey = "folder";
                    directories[pieced] = node;
                    parent.Nodes.Add(node);
                }
            }

            // Add files
            foreach (GenericFile file in Files)
            {
                string dirPath = Path.GetRelativePath(RootPath,
                    Path.GetDirectoryName(showStaging ? file.StagingOrRealPath : file.Path));
                if (dirPath == ".") dirPath = "";
                TreeNode dirNode = directories[dirPath];

                var node = new ObjectSelectorEditor.SelectorNode(showStaging ? file.StagingOrRealName : file.Name, file)
                {
                    ForeColor = Color.DimGray
                };

                node.ImageKey = node.SelectedImageKey = "file";

                if (file is AudioFile audioFile)
                {
                    if (audioFile.IsCorrupted)
                    {
                        node.ForeColor = Color.DarkMagenta;
                        
                        string corruptReasonText = string.Join(", ", audioFile.CorruptReason);
                        node.ToolTipText = "Corrupted: " + corruptReasonText;

                        node.ImageKey = node.SelectedImageKey = "corrupt_file";
                    }
                    else
                    {
                        switch (audioFile.TagLevel)
                        {
                            case TagsHelper.TagLevel.None:
                                node.ForeColor = Color.DarkRed;
                                node.ImageKey = node.SelectedImageKey = "bad_file";
                                break;
                            case TagsHelper.TagLevel.Basic:
                                node.ForeColor = Color.DarkOrange;
                                node.ImageKey = node.SelectedImageKey = "ok_file";
                                break;
                            case TagsHelper.TagLevel.Acceptable:
                                node.ForeColor = Color.DarkGreen;
                                node.ImageKey = node.SelectedImageKey = "good_file";
                                break;

                            case TagsHelper.TagLevel.Comprehensive:
                                node.ForeColor = Color.DarkGreen;
                                node.ImageKey = node.SelectedImageKey = "great_file";
                                break;
                        }
                    }
                }

                dirNode.Nodes.Add(node);
            }

            // Add workspace node to tree view
            tree.BeginUpdate();
            tree.Nodes.Clear();
            tree.Nodes.Add(workspaceNode);
            tree.ExpandAll();
            tree.EndUpdate();
        }

        public void Save(string path)
        {
            JObject obj = new JObject();
            obj.Add("_version", FormatVersion);
            obj.Add("_date_modified", DateTime.Now);
            obj.Add("workspace", JObject.FromObject(this, JsonSerializer.Create(JsonSettings)));
            
            File.WriteAllText(path, obj.ToString());
        }

        public static Workspace Load(string path)
        {
            JObject obj = JObject.Parse(File.ReadAllText(path));

            if (obj.GetValue("_version").Value<int>() != FormatVersion)
                throw new Exception("Workspace not compatible with this version of IdSort.");

            return obj.GetValue("workspace").ToObject<Workspace>(JsonSerializer.Create(JsonSettings));
        }

        private class WorkspaceNode : IContextMenu
        {
            private readonly Workspace _workspace;
            private readonly TreeView _treeView;

            public WorkspaceNode(Workspace workspace, TreeView treeView)
            {
                _workspace = workspace;
                _treeView = treeView;
            }

            public IEnumerable<ToolStripItem> GetContextMenuItems()
            {
                yield return new ToolStripMenuItem("Show in Explorer", null, (s, a) =>
                {
                    Process.Start("explorer", _workspace.RootPath);
                });

                yield return new ToolStripMenuItem("Expand All", null, (s, a) =>
                {
                    _treeView.BeginUpdate();
                    _treeView.ExpandAll();
                    _treeView.EndUpdate();
                });

                yield return new ToolStripMenuItem("Collapse All", null, (s, a) =>
                {
                    _treeView.CollapseAll();
                    _treeView.Nodes[0].Expand();
                });
            }
        }
    }
}
