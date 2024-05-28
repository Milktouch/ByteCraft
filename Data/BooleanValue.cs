using ByteCraft.Data.OtherQualities;

namespace ByteCraft.Data{
    public class BooleanValue : Value , IStringable
    {
        public BooleanValue(bool value):base(value,ValueTypes.BOOLEAN)
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

        public StringValue ToStr()
        {
            return new StringValue(this.value.ToString());
        }
    }
}