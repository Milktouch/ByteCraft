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
        public string operationName { get; internal set; }
        public List<Value> arguments { get; internal set; }

    }
}
