namespace ByteCraft.Data.Equality
{
    public interface InEquality<T>
    {
        public Value<bool> GreaterThan(Value<T> value);
        public Value<bool> LessThan(Value<T> value);
        public Value<bool> GreaterThanOrEqual(Value<T> value);
        public Value<bool> LessThanOrEqual(Value<T> value);
    }
}