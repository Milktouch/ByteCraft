using System.Runtime.CompilerServices;
using ByteCraft.Data;
using ByteCraft.Scopes;

namespace ByteCraft
{
    public class Variable
    {
        public readonly string name;
        private Value value;
        internal Variable(string name)
        {
            if (name == null)
            {
                this.name = "";
            }
            else
            {
                this.name = name;
            }
            this.value = new NullValue();

        }
        internal Variable(string name, Value value)
        {
            this.name = name;
            this.value = value;
        }
        public virtual bool AreEqual(Variable variable)
        {
            return GetValue().AreEqual(variable.GetValue());
        }

        public virtual bool IsNull()
        {
            return GetValue().IsNull();
        }

        public Variable Copy(string name)
        {
            return new Variable(name, value.Copy());
        }

        public Value GetValue()
        {
            return value;
        }


        public void SetValue(Value value)
        {
            this.value = value;
        }

    }
}