namespace ByteCraft.Data{
    public class BooleanValue : Value
    {
        internal BooleanValue(bool value):base(value,ValueTypes.BOOLEAN)
        {
        }

        public bool GetBoolean()
        {
            return this.value;
        }

        public override Value Copy()
        {
            return new BooleanValue(this.value);
        }
    }
}