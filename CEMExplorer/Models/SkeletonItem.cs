namespace CEMExplorer.Models
{
    internal sealed class SkeletonItem
    {
        public SkeletonItem(int depth, string relativePath, bool isDirectory)
        {
            Depth = depth;
            RelativePath = relativePath;
            IsDirectory = isDirectory;
        }

        public int Depth { get; }
        public string RelativePath { get; }
        public bool IsDirectory { get; }
    }
}
