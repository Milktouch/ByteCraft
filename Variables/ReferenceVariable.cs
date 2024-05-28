using ByteCraft.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ByteCraft.Variables
{
    public class ReferenceVariable : Variable
    {
        internal Variable reference;
        internal ReferenceVariable(string name) : base(name)
        {
            reference = new ValueVariable(name);
        }
        internal ReferenceVariable(string name, Variable reference) : base(name)
        {
            this.reference = reference;
        }

        public override Variable Copy(string name)
        {
            return new ReferenceVariable(name, reference);
        }

        public override Value GetValue()
        {
            return reference.GetValue();
        }

        public override ReferenceVariable Ref()
        {
            return reference.Ref();
        }

        public override void SetValue(Value value)
        {
            reference.SetValue(value);
        }

        public Variable Dereference()
        {
            return reference;
        }
    }
}
