namespace ByteCraft.Data
{
    public class Value<T>
    {

        public static readonly NullValue NULL = new NullValue();
        public string type{get;protected set;}
        public T value{get;internal set;}
        internal Value(T value, string type)
        {
            if(value == null)
                throw new System.ArgumentNullException();
            this.value = value;
            this.type = type;
        }
        public virtual bool AreEqual(Value<dynamic> value){
            if(value.IsNull() && !this.IsNull())
                return false;
            if(!value.IsNull() && this.IsNull())
                return false;
            Value<dynamic> v1 = value;
            Value<dynamic> v2 = this as Value<dynamic>;
            while(v1.type == "Reference")
                v1 = v1.value;
            while(v2.type == "Reference")
                v2 = v2.value;
            return v1.value.Equals(v2.value) && v1.type == v2.type;
        }
        public static Value<bool> CreateValue(bool value){
            return new BooleanValue(value);
        }
        public static Value<decimal> CreateValue(decimal value){
            return new NumberValue(value);
        }
        public static Value<string> CreateValue(string value){
            return new StringValue(value);
        }

        public bool IsNull(){
            return this.type == NULL.type;
        }
        public Value<T> Copy()
        {
            return new Value<T>(this.value, this.type);
        }

    }
}