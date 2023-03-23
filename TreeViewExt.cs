using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace uk.andyjohnson.XmpLibeRator
{
    // TreeView control uses a non-LINQable collection class for its nodes. These extensiopn methods hide that somewhat.

    public static class TreeViewExt
    {
        /// <summary>
        /// Get root nodes as a enumerable collection
        /// </summary>
        /// <returns>Enumerable collection of root TreeNode objects</returns>
        public static IEnumerable<TreeNode> GetRootNodes(
            this TreeView self)
        {
            var rootNodes = new TreeNode[self.Nodes.Count];
            self.Nodes.CopyTo(rootNodes, 0);
            return rootNodes;
        }
    }


    public static class TreeNodeExt
    {
        /// <summary>
        /// Return a enumerable collection of the node and all its child nodes.
        /// </summary>
        /// <returns>Enumerable collection of the node and all its child nodes</returns>
        public static IEnumerable<TreeNode> GetSelfAndChildren(
            this TreeNode self)
        {
            var nodes = new List<TreeNode>();
            nodes.Add(self);
            for(int i = 0; i < self.Nodes.Count; i++)
            {
                nodes.AddRange(GetSelfAndChildren(self.Nodes[i]));
            }
            return nodes;
        }


        /// <summary>
        /// Return the node and any of its children that have a specified checked state.
        /// </summary>
        /// <param name="isChecked">State to return</param>
        /// <returns>The node and any of its children that have the specified checked state</returns>
        public static IEnumerable<TreeNode> GetAllChecked(
            this TreeNode self,
            bool isChecked)
        {
            var checkedNodes = new List<TreeNode>();
            if (self.Checked == isChecked)
                checkedNodes.Add(self);
            for(int i = 0; i < self.Nodes.Count; i++)
            {
                checkedNodes.AddRange(GetAllChecked(self.Nodes[i], isChecked));
            }
            return checkedNodes;
        }


        /// <summary>
        /// Set the checked state of the node and its children
        /// </summary>
        /// <param name="isChecked">Checked state to set</param>
        public static void SetAllChecked(
            this TreeNode self,
            bool isChecked)
        {
            self.Checked = isChecked;
            for (int i = 0; i < self.Nodes.Count; i++)
            {
                self.Nodes[i].SetAllChecked(isChecked);
            }
        }
    }


    public static class TreeNodeCollectionExt
    {
        /// <summary>
        /// Return the first node in the collection that has the specified text
        /// </summary>
        /// <param name="text">Text to find</param>
        /// <returns>Matching node or null if no node matches</returns>
        public static TreeNode? FindByText(
            this TreeNodeCollection self,
            string text)
        {
            for(int i = 0; i < self.Count; i++)
            {
                if (self[i].Text == text)
                    return self[i];
            }
            return null;
        }


        /// <summary>
        /// Return the first node in the collection that has the specified tag.
        /// Note that if the tag parameter is non-null then comparison is done by string equality.
        /// If the tag parameter is null then returns the first node wth a null tag.
        /// </summary>
        /// <param name="tag">Tag to match</param>
        /// <returns>Matching node or null if no node matches</returns>
        public static TreeNode? FindByTag(
            this TreeNodeCollection self,
            object tag)
        {
            for (int i = 0; i < self.Count; i++)
            {
                if ( (tag == null && self[i].Tag == null) || (self[i].Tag?.ToString() == tag?.ToString()) )
                    return self[i];
            }
            return null;
        }
    }
}
