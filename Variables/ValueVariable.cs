using ByteCraft.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ByteCraft.Variables
{
    public class ValueVariable : Variable
    {
        internal Value value;
        internal ValueVariable(string name) : base(name)
        {
            value = new NullValue();
        }
        internal ValueVariable(string name, Value value) : base(name)
        {
            this.value = value;
        }

        public override Variable Copy(string name)
        {
            return new ValueVariable(name, value.Copy());
        }

        public override Value GetValue()
        {
            return value;
        }


        public override ReferenceVariable Ref()
        {
            return new ReferenceVariable(name, this);
        }

        public override void SetValue(Value value)
        {
            this.value = value;
        }
    }
}
