namespace ByteCraft.Data
{
    public interface Indexable<T>
    {
        public void SetValueAt(int index, Value<T> value);
        public Value<T> GetValueAt(int index);
        public NumberValue Length();
        
    }
}