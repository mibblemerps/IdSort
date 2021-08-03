using System.Collections.Generic;
using System.Windows.Forms;

namespace IdSort
{
    public static class TreeViewHelper
    {
        public static IEnumerable<TreeNode> GetAllNodes(this TreeView treeView)
        {
            foreach (TreeNode node in treeView.Nodes)
            {
                yield return node;

                foreach (TreeNode child in GetAllNodes(node))
                    yield return child;
            }
        }

        private static IEnumerable<TreeNode> GetAllNodes(this TreeNode parent)
        {
            foreach (TreeNode node in parent.Nodes)
            {
                yield return node;

                foreach (TreeNode child in GetAllNodes(node))
                    yield return child;
            }
        }
    }
}
