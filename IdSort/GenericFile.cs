using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;

namespace IdSort
{
    public class GenericFile : IContextMenu
    {
        /// <summary>
        /// Current path on the file system.
        /// </summary>
        public string Path;

        /// <summary>
        /// Path the file is intended to be moved to. Null if no staging path set.
        /// </summary>
        public string StagingPath;

        public string Name =>
            Path.Split(System.IO.Path.DirectorySeparatorChar, System.IO.Path.AltDirectorySeparatorChar).LastOrDefault();

        public virtual string Description => "File";

        public string StagingOrRealPath => StagingPath ?? Path;

        public string StagingOrRealName => StagingOrRealPath
            .Split(System.IO.Path.DirectorySeparatorChar, System.IO.Path.AltDirectorySeparatorChar).LastOrDefault();

        public GenericFile(string path)
        {
            Path = path;
        }

        public IEnumerable<ToolStripItem> GetContextMenuItems()
        {
            yield return new ToolStripMenuItem("Open", null, (s, a) =>
            {
                Process.Start("explorer", Path);
            });

            yield return new ToolStripMenuItem("Show in Explorer", null, (s, a) =>
            {
                Process.Start("explorer", $"/select,\"{Path}\"");
            });
        }
    }
}
