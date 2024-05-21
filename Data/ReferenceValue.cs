namespace ByteCraft.Data
{
    public class ReferenceValue : Value<Value<dynamic>>
    {
        public ReferenceValue(Value<dynamic> value) : base(value, ValueTypes.REFERENCE)
        {
        }

        public Value<dynamic> DereferenceValue()
        {
            Value<dynamic> value = this.value;
            while (value.type == "Reference")
            {
                value = value.value;
            }
            return value;
        }

        public void Assign(Value<dynamic> newValue)
        {
            this.DereferenceValue().value = newValue.value;
        } 
    }
}