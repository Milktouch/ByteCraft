using ByteCraft.Data;
using ByteCraft.Variables;

namespace ByteCraft.Operations
{
    public abstract class Operation
    {
        public string name { get; }
        public string returnType { get; }
        public List<string> parameterTypes { get; }
        public abstract Value Execute(List<Variable> parameters);


    }
}
