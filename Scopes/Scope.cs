
using ByteCarft;
using ByteCraft.Variables;

namespace ByteCraft.Scopes
{
    public class Scope
    {
        //static
        private static Stack<Scope> scopeStack = new Stack<Scope>();
        public static Scope CurrentScope
        {
            get
            {
                if (scopeStack.Count == 0)
                    return null;
                return scopeStack.Peek();
            }
        }
        public static Scope Get()
        {
            return CurrentScope;
        }
        //instance
        private readonly Dictionary<string, Variable> variables = new Dictionary<string, Variable>();
        public Variable Result { get; internal set; }
        public CodeFile file { get; private set; }
        public long line { get; private set; }
        public Scope(CodeFile file, long line)
        {
            scopeStack.Push(this);
            Result = new ValueVariable("Result");
            this.file = file;
            this.line = line;
        }

        internal void AddVariable(Variable variable)
        {
            variables.Add(variable.name, variable);
        }

        internal Variable GetVariable(string name)
        {
            if (variables.ContainsKey(name))
            {
                return variables[name];
            }
            return new ValueVariable(name);
        }

        internal void Exit()
        {
            scopeStack.Pop();
        }

    }
}
