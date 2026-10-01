using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CEMExplorer.Models;

namespace CEMExplorer.Services
{
    internal sealed class SystemArchitectureService
    {
        public string Create(string projectRoot, string title, string modelVersion, TreeView selectedOutline)
        {
            string name = SafePart(title);
            string version = string.IsNullOrWhiteSpace(modelVersion) ? string.Empty : "-" + SafePart(modelVersion);
            string suffix = name + version;
            string conceptFolder = Path.Combine(projectRoot, "docs", "ConceptArchitecture");
            if (!Directory.Exists(conceptFolder))
                throw new DirectoryNotFoundException("The project's docs/ConceptArchitecture folder was not found.");

            string skeleton = Path.Combine(AppContext.BaseDirectory, "CEMEXPLORERSKELETON.txt");
            IReadOnlyList<SkeletonItem> items = new SkeletonService().Parse(skeleton, new DirectoryInfo(projectRoot).Name);
            int templateIndex = -1;
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].IsDirectory && Path.GetFileName(items[i].RelativePath).Contains("SystemArchitecture-[xxxxxxxx]"))
                {
                    templateIndex = i;
                    break;
                }
            }
            if (templateIndex < 0)
                throw new InvalidDataException("The system architecture template is missing from the CEM skeleton.");

            SkeletonItem template = items[templateIndex];
            string destination = Path.Combine(conceptFolder, "SystemArchitecture-" + suffix);
            if (Directory.Exists(destination) || File.Exists(destination))
                throw new IOException("A system architecture with this title and model/version already exists: " + destination);

            // Validate all names and the selected outline before creating anything on disk.
            List<(string path, bool directory)> entries = new List<(string, bool)>();
            string prefix = template.RelativePath + Path.DirectorySeparatorChar;
            foreach (SkeletonItem item in items.Skip(templateIndex + 1))
            {
                if (!item.RelativePath.StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
                    break;
                string relative = item.RelativePath.Substring(prefix.Length);
                if (Path.GetFileName(relative) == "@folders-first")
                    continue;
                relative = relative.Replace("xxxxxxxx", suffix, StringComparison.OrdinalIgnoreCase);
                foreach (string part in relative.Split(Path.DirectorySeparatorChar))
                    ValidatePart(part);
                entries.Add((Path.Combine(destination, relative), item.IsDirectory));
            }

            using TreeView filtered = new TreeView();
            foreach (TreeNode root in selectedOutline.Nodes)
            {
                TreeNode? copy = CopyChecked(root);
                if (copy != null)
                    filtered.Nodes.Add(copy);
            }
            if (filtered.Nodes.Count == 0)
                throw new InvalidOperationException("Select at least one Concept item before creating the system architecture.");

            bool created = false;
            try
            {
                Directory.CreateDirectory(destination);
                created = true;
                foreach (var entry in entries)
                {
                    if (entry.directory)
                        Directory.CreateDirectory(entry.path);
                    else
                    {
                        Directory.CreateDirectory(Path.GetDirectoryName(entry.path)!);
                        if (Path.GetExtension(entry.path).Equals(".ceo", StringComparison.OrdinalIgnoreCase))
                            new CeoOutlineService().Save(entry.path, filtered);
                        else if (Path.GetFileName(entry.path).StartsWith("_SystemOverview-", StringComparison.OrdinalIgnoreCase))
                            File.WriteAllText(entry.path, "# " + title.Trim() + Environment.NewLine +
                                (modelVersion.Length == 0 ? "" : Environment.NewLine + "Model / Version: " + modelVersion.Trim() + Environment.NewLine), new UTF8Encoding(false));
                        else
                            File.WriteAllText(entry.path, string.Empty, new UTF8Encoding(false));
                    }
                }
                return destination;
            }
            catch
            {
                if (created)
                    Directory.Delete(destination, true);
                throw;
            }
        }

        private static TreeNode? CopyChecked(TreeNode node)
        {
            TreeNode copy = new TreeNode(node.Text);
            foreach (TreeNode child in node.Nodes)
            {
                TreeNode? selected = CopyChecked(child);
                if (selected != null)
                    copy.Nodes.Add(selected);
            }
            return node.Checked || copy.Nodes.Count > 0 ? copy : null;
        }

        private static string SafePart(string input)
        {
            string value = input.Trim().Replace(' ', '-');
            ValidatePart(value);
            return value;
        }

        private static void ValidatePart(string value)
        {
            if (string.IsNullOrWhiteSpace(value) || value == "." || value == ".." ||
                value.EndsWith(".", StringComparison.Ordinal) || value.EndsWith(" ", StringComparison.Ordinal) ||
                value.IndexOfAny(Path.GetInvalidFileNameChars()) >= 0 ||
                value.IndexOfAny(new[] { '/', '\\', ':', '*', '?', '"', '<', '>', '|' }) >= 0)
                throw new ArgumentException("The title or model/version contains characters that cannot be used in a folder name: " + value);
        }
    }
}
