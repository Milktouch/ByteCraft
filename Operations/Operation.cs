using ByteCraft.Data;
using ByteCraft.Variables;

namespace ByteCraft.Operations
{
    public abstract class Operation
    {
        public string name { get; protected set; }
        internal Variable[] arguments { get;set; } 
        public Value GetArgumentByName(string name)
        {
            foreach (var arg in arguments)
            {
                if (arg.name == name)
                {
                    return arg.GetValue();
                }
            }
            return Value.Null();
        }
        public abstract Value Execute();



    }
}
