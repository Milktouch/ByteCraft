using ByteCraft.Data.OtherQualities;
using ByteCraft.Operations;

namespace ByteCraft.Data;

public class OperationValue : Value 
{
    internal readonly OperationDefinition value;
    internal OperationValue(OperationDefinition value) : base(ValueTypes.OPERATION)
    {
        this.value = value;
    }

    public override Value Copy()
    {
        return new OperationValue(value);
    }

    public override string ToString()
    {
        return (value as OperationDefinition).opName;
    }
    
    public Value ExecuteOperation(Value[] args)
    {
        OperationDefinition op = value;
        return op.ExecuteOperation(args);
    }
    
    protected override object? GetValue()
    {
        return value;
    }
}