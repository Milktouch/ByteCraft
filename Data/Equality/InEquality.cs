namespace ByteCraft.Data.Equality
{
    public interface InEquality
    {
        public BooleanValue GreaterThan(Value value);
        public BooleanValue LessThan(Value value);
        public BooleanValue GreaterThanOrEqual(Value value);
        public BooleanValue LessThanOrEqual(Value value);
    }
}