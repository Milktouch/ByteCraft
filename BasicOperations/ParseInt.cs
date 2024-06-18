using ByteCraft.Data;
using ByteCraft.Exceptions;
using ByteCraft.Operations;

namespace ByteCraft.BasicOperations;

[Operation("int", "this operation parses the string argument to an integer")]
public class ParseInt : Operation
{
    public override Value Execute()
    {
        Value arg = GetArgument(0);
        if(arg==null)
        {
            throw new RuntimeError("Int operation requires one argument");
        }
        string str = arguments[0].ToString();
        if (int.TryParse(str, out int result))
        {
            return new NumberValue(result);
        }
        else
        {
            return new NullValue();
        }
    }
}