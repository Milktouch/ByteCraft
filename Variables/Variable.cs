using System.Runtime.CompilerServices;
using ByteCraft.Data;

namespace ByteCraft.Variables
{
    public abstract class Variable
    {
        public readonly string name;
        internal Variable(string name)
        {
            if (name == null)
            {
                throw new System.ArgumentNullException("name");
            }
            this.name = name;
        }
        public virtual bool AreEqual(Variable variable)
        {
            return this.GetValue().AreEqual(variable.GetValue());
        }

        public virtual bool IsNull()
        {
            return GetValue().IsNull();
        }

        public abstract void SetValue(Value value);
        public abstract Value GetValue();
        public abstract ReferenceVariable Ref();
        public abstract Variable Copy(string name);

    }
}