using ByteCraft.Data.OtherQualities;

namespace ByteCraft.Data{
    public class BooleanValue : Value 
    {
        
        public readonly bool value;
        public BooleanValue(bool value):base(ValueTypes.BOOLEAN)
        {
            this.value = value;
        }

        public override Value Copy()
        {
            return new BooleanValue((bool)this.value);
        }

        public override string ToString()
        {
            return this.value.ToString().ToLower();
        }
        
        protected override object GetValue()
        {
            return this.value;
        }
    }
}