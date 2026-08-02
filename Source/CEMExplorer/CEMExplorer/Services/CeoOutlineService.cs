using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace CEMExplorer.Services
{
    internal sealed class CeoOutlineService
    {
        public void Load(string filePath, TreeView tree)
        {
            string[] lines = File.ReadAllLines(filePath);
            tree.Nodes.Clear();

            if (lines.Length == 0)
                return;

            List<TreeNode> parents = new List<TreeNode>();
            foreach (string sourceLine in lines)
            {
                if (string.IsNullOrWhiteSpace(sourceLine))
                    continue;

                int depth = CountDepth(sourceLine);
                string text = sourceLine.Substring(depth).Trim();
                if (text.Length == 0)
                    continue;

                TreeNode node = new TreeNode(text);
                if (depth == 0)
                {
                    if (tree.Nodes.Count > 0)
                        throw new InvalidDataException("A .ceo outline can contain only one root item.");
                    tree.Nodes.Add(node);
                    parents.Clear();
                    parents.Add(node);
                    continue;
                }

                if (tree.Nodes.Count == 0)
                    throw new InvalidDataException("A .ceo outline must begin with a root name on its own line.");
                if (depth > parents.Count)
                    throw new InvalidDataException("A .ceo outline skips a hierarchy level: " + sourceLine);

                while (parents.Count > depth)
                    parents.RemoveAt(parents.Count - 1);

                TreeNode parent = parents[depth - 1];
                parent.Nodes.Add(node);
                if (parents.Count == depth)
                    parents.Add(node);
                else
                    parents[depth] = node;
            }

            tree.ExpandAll();
            if (tree.Nodes.Count > 0)
                tree.SelectedNode = tree.Nodes[0];
        }

        public void Save(string filePath, TreeView tree)
        {
            List<string> lines = new List<string>();
            foreach (TreeNode node in tree.Nodes)
                AppendNode(lines, node, 0);

            File.WriteAllLines(filePath, lines, new UTF8Encoding(false));
        }

        private static void AppendNode(List<string> lines, TreeNode node, int depth)
        {
            lines.Add(new string('-', depth) + node.Text.Trim());
            foreach (TreeNode child in node.Nodes)
                AppendNode(lines, child, depth + 1);
        }

        private static int CountDepth(string line)
        {
            int depth = 0;
            while (depth < line.Length && line[depth] == '-')
                depth++;
            return depth;
        }
    }
}
