namespace ByteCraft.Data.Arithmetic
{
    public interface Multiplication<T> where T : Value
    {
        public T Multiply(T val);
    }
}