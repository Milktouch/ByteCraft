using ByteCraft.Data.OtherQualities;

namespace ByteCraft.Data{
    public class BooleanValue : Value 
    {
        public BooleanValue(bool value):base(value,ValueTypes.BOOLEAN)
        {
        }

        public bool GetBoolean()
        {
            return (bool)this.value;
        }

        public override Value Copy()
        {
            return new BooleanValue((bool)this.value);
        }

        public override string ToString()
        {
            return this.value.ToString().ToLower();
        }
    }
}