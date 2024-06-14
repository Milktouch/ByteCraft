using ByteCarft;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ByteCraft.Scopes
{
    internal class FileScope
    {
        private static Dictionary<string, FileScope> fileScopes = new Dictionary<string, FileScope>(); 
        private FileScope(string fileName)
        {
            this.fileName = fileName;
        }

        internal static FileScope GetFileScope(string fileName)
        {
            if (!fileScopes.ContainsKey(fileName))
            {
                fileScopes.Add(fileName, new FileScope(fileName));
            }
            return fileScopes[fileName];
        }

        internal readonly Dictionary<string, Variable> variables = new Dictionary<string, Variable>();
        public string fileName { get; private set; }

        internal void AddVariable(Variable variable)
        {
            variables.Add(variable.name, variable);
        }
    }
}
