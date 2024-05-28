using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ByteCraft.Data
{
    public class ReferenceValue : Value
    {
        public ReferenceValue(Value v) : base(v,ValueTypes.REFERENCE)
        { 
        }
        public override Value Copy()
        {
            return this;
        }
    }
}
