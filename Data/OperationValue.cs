using ByteCraft.Data.OtherQualities;
using ByteCraft.Operations;

namespace ByteCraft.Data;

public class OperationValue : Value 
{
    internal OperationValue(OperationDefinition value) : base(value, ValueTypes.OPERATION)
    {
    }

    public OperationValue(string type) : base(type)
    {
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
        OperationDefinition op = value as OperationDefinition;
        return op.ExecuteOperation(args);
    }
}