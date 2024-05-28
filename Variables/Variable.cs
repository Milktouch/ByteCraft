using System.Runtime.CompilerServices;
using ByteCraft.Data;
using ByteCraft.Scopes;

namespace ByteCraft.Variables
{
    public abstract class Variable
    {
        public readonly string name;
        internal Variable(string name)
        {
            if (name == null)
            {
                this.name = "";
            }
            else
            {
                this.name = name;
                Scope.CurrentScope.AddVariable(this);
            }
            
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