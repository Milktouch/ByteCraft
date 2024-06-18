using ByteCraft.Exceptions;

namespace ByteCraft.Data
{
    public abstract class Value
    {

        public readonly string type;
        protected dynamic value{get;set;}
        internal Value(dynamic value, string type)
        {
            if(value == null && type != ValueTypes.NULL)
                throw new System.ArgumentNullException();
            this.value = value;
            this.type = type;
        }
        internal Value(string type)
        {
            this.type = type;
        }
        public virtual bool AreEqual(Value value){
            if(value.type != this.type)
                return false;
            return value.value == this.value;
        }
        public static BooleanValue CreateValue(bool value){
            return new BooleanValue(value);
        }
        public static NumberValue CreateValue(decimal value){
            return new NumberValue(value);
        }
        public static StringValue CreateValue(string value){
            return new StringValue(value);
        }
        public bool IsNull(){
            return this.type == ValueTypes.NULL;
        }
        public static NullValue Null()
        {
            return new NullValue();
        }
        public abstract Value Copy();

        public bool CanBeCastTo<T>() where T : Value
        {
            try
            {
                T val = (T)this;
                return true;
            }
            catch (InvalidCastException)
            {
                return false;
            }
        }

        public T As<T>() where T : Value
        {
            if(CanBeCastTo<T>())
                return (T)this;
            throw new RuntimeError(type +" cannot be cast into " + typeof(T).Name);
        }

        public abstract override string ToString();

    }
}