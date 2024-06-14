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
            Value? val = GetArgument(0);
            if (val == null) {
                throw new RuntimeError("Write operation requires a string argument");
            }
            if (val is IStringable)
            {
                Console.WriteLine(((IStringable)val).ToString());
            }
            else
            {
                throw new RuntimeError($"argument of type {val.type} cannot be converted to string");
            }
            return new NullValue();
        }
    }
}
