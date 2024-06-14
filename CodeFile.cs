
using ByteCraft.Scopes;

namespace ByteCarft
{
    internal class CodeFile
    {
        public readonly string[] lines;
        public readonly FileScope fileScope;
        public readonly FileInfo fileInfo;
        public CodeFile(FileInfo fileInfo)
        {
            this.fileInfo = fileInfo;
            lines=File.ReadAllLines(fileInfo.FullName);
            fileScope = FileScope.GetFileScope(fileInfo.FullName);
        }
    }
}
