using ByteCraft.Data.Arithmetic;
using ByteCraft.Data.OtherQualities;
using ByteCraft.Exceptions;

namespace ByteCraft.Data;

public class ListValue : Value , Indexable , Addition
{
    private List<Value> list;
    public ListValue() : base(ValueTypes.LIST)
    {
        list = new List<Value>();
    }

    public override Value Copy()
    {
        ListValue newList = new ListValue();
        foreach (Value val in list)
        {
            newList.list.Add(val.Copy());
        }
        return newList;
    }

    public override string ToString()
    {
        string str = "{";
        foreach (Value val in list)
        {
            str += val.ToString() + ",";
        }
        if (str.Length > 1)
        {
            str = str.Remove(str.Length - 1);
        }
        str += "}";
        return str;
    }

    protected override object? GetValue()
    {
        return list;
    }

    public void SetValueAt(NumberValue index, Value value)
    {
        int i = (int)index.value;
        if (i < 0 || i >= list.Count)
        {
            throw new RuntimeError($"Index {i} is  out of bounds (Current Length is {Length().value}");
        }
        list[i] = value;
    }

    public Value GetValueAt(NumberValue index)
    {
        int i = (int)index.value;
        if (i < 0 || i >= list.Count)
        {
            throw new RuntimeError($"Index {i} is  out of bounds (Current Length is {Length().value}");
        }
        return list[i];
    }

    public NumberValue Length()
    {
        return new NumberValue(list.Count);
    }

    public Value Add(Value val)
    {
        Insert(val);
        return this;
    }
    
    public void Insert(Value value)
    {
        list.Add(value);
    }
    
    public void RemoveAt(int index)
    {
        int i = (int)index;
        if (i < 0 || i >= list.Count)
        {
            throw new RuntimeError($"Index {i} is  out of bounds (Current Length is {Length().value}");
        }
        list.RemoveAt(i);
    }
    
    public void InsertAt(int index, Value value)
    {
        int i = (int)index;
        if (i < 0 || i >= list.Count)
        {
            throw new RuntimeError($"Index {i} is  out of bounds (Current Length is {Length().value}");
        }
        list.Insert(i, value);
    }
    
    public void Remove(Value value)
    {
        list.Remove(value);
    }
    
}