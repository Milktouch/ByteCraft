namespace ByteCraft.Data.OtherQualities
{
    public interface Indexable
    {
        public void SetValueAt(NumberValue index, Value value);
        public Value GetValueAt(NumberValue index);
        public NumberValue Length();

    }
}