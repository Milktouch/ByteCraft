using ByteCraft.Data;
using ByteCraft.Data.OtherQualities;
using ByteCraft.Exceptions;
using ByteCraft.Operations;
using ByteCraft.Variables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ByteCraft.BuiltInOps
{
    public class WriteOp : Operation
    {
        public WriteOp()
        {
            name = "write";
        }

        public override Value Execute()
        {
            if (arguments.Length == 1)
            {
                throw new RuntimeError($"Write requires at least one argument");
            }
            StringBuilder strBuilder = new StringBuilder();

            for (int i = 0; i < arguments.Length; i++)
            {
                Value value = arguments[i].GetValue();
                if (value is IStringable)
                {
                    IStringable stringable = value as IStringable;
                    strBuilder.Append(stringable.ToStr().GetStringValue());
                    strBuilder.Append(" ");
                }
                else
                {
                    throw new RuntimeError($"all provided arguments must be able to be converted to string. \n value type of {value.type} cannot be converted to a string value");
                }
                    
            }
            Console.WriteLine(strBuilder.ToString());
            return Value.Null();
        }
    }
}
