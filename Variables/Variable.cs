using System.Runtime.CompilerServices;
using ByteCraft.Data;

namespace ByteCraft.Variables
{
    public abstract class Variable
    {
        public readonly string name;
        public Value<dynamic> value { protected set; get;}
        public Variable(string name, Value<dynamic> value)
        {
            this.name = name;
            this.value = value;
        }
        public Variable(string name)
        {
            this.name = name;
            this.value = Value<dynamic>.NULL;
        }
        
        public virtual bool AreEqual(Variable variable){
            return this.value.AreEqual(variable.value);
        }

        public bool IsNull(){
            return this.value.IsNull();
        }

        public void SetValue(Value<dynamic> value)
        {
            this.value = value;
        }


    }
}