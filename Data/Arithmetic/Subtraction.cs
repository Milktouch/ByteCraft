namespace ByteCraft.Data.Arithmetic
{
    public interface Subtraction<T> where T : Value
    {
        public T Subtract(T val);
    }
}