using System.Collections.Generic;
using System.Windows.Forms;

namespace IdSort
{
    public interface IContextMenu
    {
        IEnumerable<ToolStripItem> GetContextMenuItems();
    }
}
