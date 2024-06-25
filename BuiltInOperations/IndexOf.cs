using ByteCraft.Data;
using ByteCraft.Data.OtherQualities;
using ByteCraft.Exceptions;
using ByteCraft.Operations;

namespace ByteCraft.BuiltInOperations;

[Operation("IndexOf", "returns the index of the first occurence of certain value in an indexable value")]
internal class IndexOf : Operation
{
    public override Value Execute()
    {
        if(arguments.Length < 2)
        {
            throw new RuntimeError("IndexOf operation requires an indexable value as the first argument and a value as the second argument");
        }
        var indexable = GetArgument(1);
        var value = GetArgument(1);

        if (indexable is not Indexable indexableValue || value is null || value is not NumberValue)
        {
            throw new RuntimeError("IndexOf operation requires an indexable value as the first argument and a value as the second argument");
        }

        for(int i = 0; i < (int)indexableValue.Length().value; i++)
        {
            if(indexableValue.GetValueAt(new NumberValue(i)).AreEqual(value))
            {
                return new NumberValue(i);
            }
        }

        return new NumberValue(-1);
    }
}