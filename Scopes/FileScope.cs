using ByteCraft;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ByteCraft.Exceptions;

namespace ByteCraft.Scopes
{
    internal class FileScope : Scope
    {
        private static Dictionary<string, FileScope> fileScopes = new Dictionary<string, FileScope>();
        
        private readonly CodeFile codeFile;
        private FileScope(string filename) : base(CodeFile.GetCodeFile(filename),null)
        {
            this.codeFile = CodeFile.GetCodeFile(filename);
            if (codeFile==null)
            {
                throw new RuntimeError("File not found: " + filename);
            }
        }
        internal static FileScope GetFileScope(string fileName)
        {
            if (!fileScopes.ContainsKey(fileName))
            {
                fileScopes.Add(fileName, new FileScope(fileName));
            }
            return fileScopes[fileName];
        }

        internal override Variable? GetVariable(string name)
        {
            return codeFile.fileVariables[name];
        }

        internal override void SetVariable(Variable variable)
        {
            if (!codeFile.fileVariables.ContainsKey(variable.name))
            {
                codeFile.fileVariables.Add(variable.name,variable);
            }
        }
    }
}
