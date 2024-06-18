using ByteCraft.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ByteCraft.Parsing
{
    internal class OperationLine : CodeLine
    {
        public readonly string operationName;
        public readonly List<Value> arguments;
        
        public OperationLine(string name,List<Value> args) : base(LineType.Operation)
        {
            operationName = name;
            arguments = args;
        }

        public override string ToString()
        {
            List<string> argsAsString = new();
            foreach (Value arg in arguments)
            {
                argsAsString.Add(arg.ToString());
            }
            return $"operation {operationName} with arguments {string.Join(", ", arguments)}";
        }
    }
}
