using ByteCraft.Data.Arithmetic;
using ByteCraft.Data.OtherQualities;
using ByteCraft.Exceptions;

namespace ByteCraft.Data
{
    public class StringValue : Value, Indexable , Addition
    {
        public StringValue(string value) : base(value, ValueTypes.STRING)
        {
        }

        public override Value Copy()
        {
            return new StringValue((String)this.value);
        }

        public Value GetValueAt(NumberValue val)
        {
            decimal index = val.GetNumber();
            if (index < 0 || index >= value.Length)
            {
                throw new RuntimeError($"Index {index} is  out of bounds (Current Length is {Length().GetNumber()}");
            }
            return new StringValue(value[(int)index].ToString());
        }
        public NumberValue Length()
        {
            return new NumberValue(value.Length);
        }
        public void SetValueAt(NumberValue index, Value otherVal)
        {
            string other = otherVal.ToString();
            string newString = ToString();
            int i = (int)index.GetNumber();
            if (i < 0 || i >= newString.Length)
            {
                throw new RuntimeError($"Index ({i}) is  out of bounds (Current Length is {Length().GetNumber()}");
            }
            newString = newString.Remove(i,1).Insert(i, other);
            value = newString;
        }

        public override string ToString()
        {
            return this.value;
        }

        public Value Add(Value val)
        {
            return new StringValue(ToString()+val.ToString());
        }
        
    }
}