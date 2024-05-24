namespace ByteCraft.Data.Arithmetic{
    public interface IAddition<T> where T : Value
    {
        public T Add(T val);
    } 
}