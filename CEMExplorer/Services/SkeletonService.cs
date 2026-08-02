using System;
using System.Collections.Generic;
using System.IO;
using CEMExplorer.Models;

namespace CEMExplorer.Services
{
    internal sealed class SkeletonService
    {
        public IReadOnlyList<SkeletonItem> Parse(string skeletonFile, string abbreviation)
        {
            if (!File.Exists(skeletonFile))
                throw new FileNotFoundException("The CEM Explorer skeleton file was not found.", skeletonFile);

            List<SkeletonItem> items = new List<SkeletonItem>();
            List<string> directoryStack = new List<string>();

            foreach (string sourceLine in File.ReadAllLines(skeletonFile))
            {
                if (string.IsNullOrWhiteSpace(sourceLine))
                    continue;

                int depth = CountIndent(sourceLine);
                string name = sourceLine.Trim().TrimEnd('|').Trim();
                if (name.Length == 0 || name == "|")
                    continue;

                bool isDirectory = name.EndsWith("/", StringComparison.Ordinal) ||
                                   name.EndsWith("\\", StringComparison.Ordinal);
                name = name.TrimEnd('/', '\\').Replace("SKLTN", abbreviation, StringComparison.OrdinalIgnoreCase);
                ValidateName(name, sourceLine);

                while (directoryStack.Count > depth)
                    directoryStack.RemoveAt(directoryStack.Count - 1);

                if (depth > directoryStack.Count)
                    throw new InvalidDataException("Invalid indentation in skeleton line: " + sourceLine);

                string relativePath = directoryStack.Count == 0
                    ? name
                    : Path.Combine(Path.Combine(directoryStack.ToArray()), name);

                items.Add(new SkeletonItem(depth, relativePath, isDirectory));

                if (isDirectory)
                {
                    if (directoryStack.Count == depth)
                        directoryStack.Add(name);
                    else
                        directoryStack[depth] = name;
                }
            }

            return items;
        }

        public string Create(string baseFolder, string skeletonFile, string abbreviation, string title)
        {
            IReadOnlyList<SkeletonItem> items = Parse(skeletonFile, abbreviation);
            if (items.Count == 0 || !items[0].IsDirectory)
                throw new InvalidDataException("The first skeleton item must be the project root folder.");

            string projectRoot = Path.GetFullPath(Path.Combine(baseFolder, items[0].RelativePath));
            if (Directory.Exists(projectRoot) && Directory.EnumerateFileSystemEntries(projectRoot).GetEnumerator().MoveNext())
                throw new IOException("The project folder already exists and is not empty: " + projectRoot);

            foreach (SkeletonItem item in items)
            {
                string fullPath = Path.GetFullPath(Path.Combine(baseFolder, item.RelativePath));
                EnsureInsideBase(baseFolder, fullPath);

                if (item.IsDirectory)
                {
                    Directory.CreateDirectory(fullPath);
                }
                else
                {
                    string? parent = Path.GetDirectoryName(fullPath);
                    if (!string.IsNullOrEmpty(parent))
                        Directory.CreateDirectory(parent);

                    if (!File.Exists(fullPath))
                        File.WriteAllText(fullPath, string.Empty);
                }
            }

            string readme = Path.Combine(projectRoot, "README.md");
            if (File.Exists(readme) && new FileInfo(readme).Length == 0 && !string.IsNullOrWhiteSpace(title))
                File.WriteAllText(readme, "# " + title.Trim() + Environment.NewLine);

            return projectRoot;
        }

        private static int CountIndent(string line)
        {
            int depth = 0;
            int spaces = 0;
            foreach (char character in line)
            {
                if (character == '\t')
                {
                    depth++;
                    spaces = 0;
                }
                else if (character == ' ')
                {
                    spaces++;
                    if (spaces == 4)
                    {
                        depth++;
                        spaces = 0;
                    }
                }
                else
                {
                    break;
                }
            }
            return depth;
        }

        private static void ValidateName(string name, string sourceLine)
        {
            if (name.IndexOfAny(Path.GetInvalidFileNameChars()) >= 0 || name == "." || name == "..")
                throw new InvalidDataException("Invalid item name in skeleton line: " + sourceLine);
        }

        private static void EnsureInsideBase(string baseFolder, string fullPath)
        {
            string normalizedBase = Path.GetFullPath(baseFolder).TrimEnd(Path.DirectorySeparatorChar) + Path.DirectorySeparatorChar;
            if (!fullPath.StartsWith(normalizedBase, StringComparison.OrdinalIgnoreCase))
                throw new InvalidDataException("A skeleton item resolves outside the selected root folder.");
        }
    }
}
