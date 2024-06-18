using ByteCraft.Data;
using ByteCraft.Exceptions;
using ByteCraft.Operations;

namespace ByteCraft.BasicOperations;

[Operation("add", "this operation adds all arguments together and returns the result")]
public class Add : Operation
{
    public override Value Execute()
    {
        Value[] values = this.arguments;
        if (values.Length<2)
        {
            throw new RuntimeError("Add operation requires at least two arguments");
        }
        decimal result = 0;
        foreach (Value value in values)
        {
            if (value.CanBeCastTo<NumberValue>())
            {
                result+= value.As<NumberValue>().GetNumber();
            }
            else
            {
                throw new RuntimeError("Add operation requires all arguments to be numbers");
            }
        }
        return Value.CreateValue(result);
    }
}