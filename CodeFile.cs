
using System.Reflection;
using ByteCraft.Operations;
using ByteCraft.Scopes;

namespace ByteCraft
{
    internal class CodeFile
    {
        private static readonly Dictionary<string,CodeFile> codeFiles = new Dictionary<string,CodeFile>();
        public readonly string[] lines;
        public readonly FileInfo fileInfo;
        private readonly FileScope fileScope;
        
        internal readonly Dictionary<string, Variable> fileVariables = new Dictionary<string, Variable>();
        private readonly Dictionary<string,OperationDefinition> operationsInFile = new Dictionary<string,OperationDefinition>();
        public CodeFile(FileInfo fileInfo)
        {
            codeFiles[fileInfo.FullName] = this;
            this.fileInfo = fileInfo;
            lines=File.ReadAllLines(fileInfo.FullName);
            fileScope = FileScope.GetFileScope(fileInfo.FullName);
        }
        public void ImportAssemblyToFile(FileInfo dll)
        {
            var asm = Assembly.LoadFrom(dll.FullName);
            var loadedOperations = OperationDefinition.LoadOperations(asm);
            foreach (var op in loadedOperations)
            {
                operationsInFile[op.opName] = op;
            }
        }
        
        public bool OperationExists(string opName)
        {
            return operationsInFile.ContainsKey(opName);
        }
        
        public OperationDefinition? GetOperationDefinition(string opName)
        {
            if (operationsInFile.ContainsKey(opName))
            {
                return operationsInFile[opName];
            }
            return null;
        }
        
        internal void AddOperation(OperationDefinition op)
        {
            operationsInFile[op.opName] = op;
        }
        
        internal static CodeFile? GetCodeFile(string filename)
        {
            return codeFiles.GetValueOrDefault(filename, null);
        }
    }
}
