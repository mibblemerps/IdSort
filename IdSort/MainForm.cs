using System;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using IdSort.Properties;
using IdSort.Restructure;

namespace IdSort
{
    public partial class MainForm : Form
    {
        /// <summary>
        /// Current workspace.
        /// </summary>
        public Workspace Workspace = new();

        public bool ShowStaging
        {
            get => _showStaging;
            set
            {
                if (value == _showStaging) return;
                _showStaging = value;
                previewFileRestructureCheckBox.Checked = _showStaging;
                ReloadCurrent();
            }
        }

        private bool _showStaging;

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            restructureControl.RequestPreviewRestructure += (o, args) =>
            {
                Restructurer.Restructure(Workspace, args.Settings, out var results);
                bool needReload = ShowStaging; // we must reload if show staging is already enabled
                ShowStaging = true;
                if (needReload)
                    ReloadCurrent();
            };
            
            currentTreeView.ImageList = new ImageList();
            currentTreeView.ImageList.Images.Add("default", Resources.blank_treeview_node);
            currentTreeView.ImageList.Images.Add("file", Resources.file_solid);
            currentTreeView.ImageList.Images.Add("bad_file", Resources.times_solid);
            currentTreeView.ImageList.Images.Add("ok_file", Resources.dash_solid);
            currentTreeView.ImageList.Images.Add("good_file", Resources.check_solid);
            currentTreeView.ImageList.Images.Add("great_file", Resources.check_double_solid);
            currentTreeView.ImageList.Images.Add("corrupt_file", Resources.exclamation_triangle_solid);
            currentTreeView.ImageList.Images.Add("folder", Resources.folder);
        }

        public void ReloadCurrent()
        {
            if (Workspace == null)
                throw new Exception("No workspace loaded");

            object previousTopNodeVal = null;
            if (currentTreeView.TopNode is ObjectSelectorEditor.SelectorNode previousTopNode)
                previousTopNodeVal = previousTopNode.value;

            object previousSelectedNodeVal = null;
            if (currentTreeView.SelectedNode is ObjectSelectorEditor.SelectorNode previousSelectedNode)
                previousSelectedNodeVal = previousSelectedNode.value;

            // Build treeview
            Workspace.BuildTreeView(currentTreeView, ShowStaging);

            TreeNode selectNode = currentTreeView.GetAllNodes().FirstOrDefault(n =>
                n is ObjectSelectorEditor.SelectorNode node && node.value == previousSelectedNodeVal);
            TreeNode topNode = currentTreeView.GetAllNodes().FirstOrDefault(n =>
                n is ObjectSelectorEditor.SelectorNode node && node.value == previousTopNodeVal);

            if (selectNode != null)
            {
                currentTreeView.SelectedNode = selectNode;
                currentTreeView.Select();
                currentTreeView.AutoScrollOffset = new Point(0, currentTreeView.AutoScrollOffset.Y);
            }
            else
            {
                fileInfoControl.SetFile(null);
            }

            if (topNode != null)
            {
                currentTreeView.TopNode = topNode;
            }
            else
            {
                currentTreeView.Nodes[0].EnsureVisible();
            }

            // Update status bar
            statusBarFilesLoaded.Text = $"{Workspace.TotalMusicFiles} audio files ({Workspace.TotalCorruptedFiles} corrupted)";
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Importer(workspace =>
            {
                Workspace = workspace;
                ReloadCurrent();

            }).ShowDialog();
        }

        private void currentTreeView_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            currentTreeView.SelectedNode = e.Node;

            if (e.Node is ObjectSelectorEditor.SelectorNode selectorNode)
            {
                if (e.Button == MouseButtons.Right)
                {
                    var context = new ContextMenuStrip();

                    if (selectorNode.value is IContextMenu contextMenu)
                    {
                        foreach (var item in contextMenu.GetContextMenuItems())
                            context.Items.Add(item);
                    }

                    if (context.Items.Count == 0) return; // No context menu items

                    context.Show(Cursor.Position);
                }
            }
        }

        private void previewFileRestructureCheckBox_CheckedChanged_1(object sender, EventArgs e)
        {
            ShowStaging = previewFileRestructureCheckBox.Checked;
        }

        private void currentTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node is ObjectSelectorEditor.SelectorNode selectorNode)
            {
                if (selectorNode.value is GenericFile file)
                {
                    fileInfoControl.SetFile(file);
                }
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new AboutForm().ShowDialog();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void commitChangesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var plan = Workspace.Plan();

            if (plan.Operations.Count == 0)
            {
                MessageBox.Show("No changes staged.", "IdSort", MessageBoxButtons.OK, MessageBoxIcon.None);
                return;
            }

            if (MessageBox.Show("Are you sure you want commit your changes to disk?\n\nYou will be able to undo these changes later.", "IdSort", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            var form = new RestructuringForm(plan);
            form.Success += (o, args) =>
            {
                Workspace.ApplyStagedChanges();
                ReloadCurrent();
            };
            form.ShowDialog();
        }

        private void revertToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string revertFilePath = Workspace.RootPath + Path.DirectorySeparatorChar + "revert.idsort";

            if (!File.Exists(revertFilePath))
            {
                MessageBox.Show($"No revert data exists for this folder. Has \"{Plan.RevertFileName}\" been misplaced?", "IdSort", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (MessageBox.Show("Are you sure you want to revert the last sort?", "IdSort", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            var plan = Plan.Load(revertFilePath);
            new RestructuringForm(plan, true).ShowDialog();
        }
    }
}
