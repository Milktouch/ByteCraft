namespace ByteCraft.Data.Equality
{
    public interface InEquality<T> where T : Value
    {
        public BooleanValue GreaterThan(T value);
        public BooleanValue LessThan(T value);
        public BooleanValue GreaterThanOrEqual(T value);
        public BooleanValue LessThanOrEqual(T value);
    }
}