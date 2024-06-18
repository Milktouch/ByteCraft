using ByteCraft.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ByteCraft.Data;
using ByteCraft.Exceptions;
using ByteCraft.Data.OtherQualities;

namespace ByteCraft.BasicOperations
{
    [Operation("write","this operation prints out the recieved string to the standard console output")]
    internal class Write : Operation
    {
        public Write() { }
        public override Value Execute()
        {
            Value[] values = this.arguments;
            if (values.Length == 0)
            {
                throw new RuntimeError("Write operation requires at least one argument");
            }
            StringBuilder sb = new();
            for (int i = 0; i < values.Length; i++)
            {
                sb.Append(values[i]);
                if (i!=values.Length-1)
                {
                    sb.Append(' ');
                }
            }
            Console.WriteLine(sb.ToString());
            return new NullValue();
        }
    }
}
