
using ByteCarft;

namespace ByteCraft.Scopes
{
    internal class Scope
    {
        public Variable Result { get; internal set; }
        internal CodeFile file { get; private set; }
        internal long line { get; private set; }
        internal readonly long startLine;
        internal readonly long endLine;
        internal Scope(CodeFile file, long line)
        {
            Result = new Variable("Result");
            AddVariable(Result);
            this.file = file;
            this.startLine = line;
        }

        internal void AddVariable(Variable variable)
        {
            file.fileScope.variables.Add(variable.name, variable);
        }

        internal Variable GetVariable(string name)
        {
            if (file.fileScope.variables.ContainsKey(name))
            {
                return file.fileScope.variables[name];
            }
            return new Variable(name);
        }


    }
}
