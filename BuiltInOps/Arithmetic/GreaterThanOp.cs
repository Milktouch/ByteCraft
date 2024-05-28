using ByteCraft.Data;
using ByteCraft.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ByteCraft.BuiltInOps.Arithmetic
{
    public class GreaterThanOp : Operation
    {
        public GreaterThanOp()
        {
        }

        public override Value Execute()
        {
            return new StringValue("GreaterThanOp");
        }
    }
}
