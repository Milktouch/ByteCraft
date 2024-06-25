using ByteCraft.Exceptions;

namespace ByteCraft.Data
{
    public class NullValue : Value
    {
        internal NullValue():base(ValueTypes.NULL)
        {
        }

        public override Value Copy()
        {
            return new NullValue();
        }

        public override string ToString()
        {
            return "";
        }

        protected override object GetValue()
        {
            return null;
        }
    }
}