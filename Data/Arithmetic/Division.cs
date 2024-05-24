namespace ByteCraft.Data.Arithmetic
{
    public interface Division<T> where T : Value
    {
        public T Divide(T val);
    }
}