namespace ByteCraft.Data.OtherQualities
{
    public interface IIndexable<T> where T : Value
    {
        public void SetValueAt(NumberValue index, T value);
        public Value GetValueAt(NumberValue index);
        public NumberValue Length();

    }
}