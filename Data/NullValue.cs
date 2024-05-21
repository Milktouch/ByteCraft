namespace ByteCraft.Data
{
    public class NullValue : Value<object>
    {
        internal NullValue():base("\0",ValueTypes.NULL)
        {
        }
    }
}