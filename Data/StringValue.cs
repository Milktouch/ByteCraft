using ByteCraft.Data.Arithmetic;
using ByteCraft.Data.OtherQualities;
using ByteCraft.Exceptions;

namespace ByteCraft.Data
{
    public class StringValue : Value, Indexable , Addition
    {
        public string value { private set; get; }
        public StringValue(string value) : base(ValueTypes.STRING)
        {
            this.value = value;
        }

        public override Value Copy()
        {
            return new StringValue(this.value);
        }

        public Value GetValueAt(NumberValue val)
        {
            decimal index = val.value;
            if (index < 0 || index >= value.Length)
            {
                throw new RuntimeError($"Index {index} is  out of bounds (Current Length is {Length().value}");
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
            int i = (int)index.value;
            if (i < 0 || i >= newString.Length)
            {
                throw new RuntimeError($"Index ({i}) is  out of bounds (Current Length is {Length().value}");
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

        protected override object? GetValue()
        {
            return value;
        }
    }
}