using ByteCraft.Data;
using ByteCraft.Data.OtherQualities;
using ByteCraft.Exceptions;
using ByteCraft.Operations;

namespace ByteCraft.BuiltInOperations;

[Operation("length","Returns the length of the list")]
internal class LenOp : Operation
{
    public override Value Execute()
    {
        Value list = GetArgument(0);
        if (list is not Indexable)
        {
            throw new RuntimeError("length operation expects an indexable value as an argument");
        }
        return ((Indexable)list).Length();
    }
}