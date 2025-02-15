
using ByteCraft;

namespace ByteCraft.Scopes
{
    internal class Scope
    {
        public readonly Variable Result;
        public int line;
        private readonly Scope? parentScope;
        private readonly Dictionary<string,Variable> scopeVariables = new();
        internal readonly CodeFile file;
        internal ScopeType scopeType;
        internal Scope(CodeFile file,Scope? parent)
        {
            this.parentScope = parent;
            this.file = file;
            Result = new Variable("Result");
            scopeVariables.Add("Result", Result);
        }
        internal virtual Variable? GetVariable(string name)
        {
            if (scopeVariables.ContainsKey(name))
            {
                return scopeVariables[name];
            }
            if (parentScope!=null)
            {
                return parentScope.GetVariable(name);
            }
            if (file.fileVariables.ContainsKey(name))
            {
                return file.fileVariables[name];
            }
            return null;
        }
        
        internal virtual void SetVariable(Variable variable)
        {
            if (!scopeVariables.ContainsKey(variable.name))
                scopeVariables.Add(variable.name, variable);
        }
        

        
        


    }
}
