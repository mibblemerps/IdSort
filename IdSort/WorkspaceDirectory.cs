using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;

namespace IdSort
{
    public class WorkspaceDirectory : IContextMenu
    {
        public string Path;

        public WorkspaceDirectory(string path)
        {
            Path = path;
        }

        public IEnumerable<ToolStripItem> GetContextMenuItems()
        {
            yield return new ToolStripMenuItem("Open in Explorer", null, (s, a) =>
            {
                Process.Start("explorer", Path);
            });
        }
    }
}
