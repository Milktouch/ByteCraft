namespace ByteCraft.Data
{
    public class NullValue : Value
    {
        internal NullValue():base("\0",ValueTypes.NULL)
        {
        }

        public override Value Copy()
        {
            return new NullValue();
        }

    }
}