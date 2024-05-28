
using ByteCraft.Scopes;

namespace ByteCarft
{
    public class CodeFile
    {
        public readonly List<string> lines = new();
        public readonly Scope fileScope;
        public readonly FileInfo fileInfo;

        public CodeFile(FileInfo fileInfo)
        {
            this.fileInfo = fileInfo;
        }

    }
}
