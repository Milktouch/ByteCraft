namespace ByteCraft.Data{
    public class BooleanValue : Value<bool>
    {
        internal BooleanValue(bool value):base(value,ValueTypes.BOOLEAN)
        {
        }
    }
}