using ByteCraft.Data.OtherQualities;
using ByteCraft.Exceptions;

namespace ByteCraft.Data
{
    public class StringValue : Value ,IIndexable<StringValue> , IStringable
    {
        public StringValue(string value) : base(value, ValueTypes.STRING)
        {
        }

        public override Value Copy()
        {
            return new StringValue(this.value);
        }

        public Value GetValueAt(NumberValue val)
        {
            decimal index = val.GetNumber();
            if (index < 0 || index >= value.Length)
            {
                throw new RuntimeError($"Index ({index}) is  out of bounds (Current Length is {Length().GetNumber()}");
            }
            return new StringValue(value[index].ToString());
        }
        public NumberValue Length()
        {
            return new NumberValue(value.Length);
        }
        public void SetValueAt(NumberValue index, StringValue otherVal)
        {
            string other = otherVal.GetStringValue();
            string newString = GetStringValue();
            int i = (int)index.GetNumber();
            if (i < 0 || i >= newString.Length)
            {
                throw new RuntimeError($"Index ({i}) is  out of bounds (Current Length is {Length().GetNumber()}");
            }
            newString = newString.Insert(i, other);
            value = newString;
        }

        public string GetStringValue()
        {
            return this.value;
        }

        public StringValue ToStr()
        {
            return this;
        }
    }
}