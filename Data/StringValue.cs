using ByteCraft.Exceptions;

namespace ByteCraft.Data
{
    public class StringValue : Value<string> ,Indexable<string>
    {
        internal StringValue(string value) : base(value, ValueTypes.STRING)
        {
        }

        public Value<string> GetValueAt(int index)
        {
            if (index < 0 || index >= value.Length)
            {
                throw new RuntimeError("Index out of bounds");
            }
            return new StringValue(value[index].ToString());
        }
        public NumberValue Length()
        {
            return new NumberValue(value.Length);
        }
        public void SetValueAt(int index, Value<string> value)
        {
            
        }
    }
}