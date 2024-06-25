using ByteCraft.Data;
using ByteCraft.Operations;

namespace ByteCraft.BasicOperations
{
    [Operation("read", "this operation reads a string from the standard console input")]
    internal class Read : Operation
    {
        public Read() { }
        public override Value Execute()
        {
            string? input = Console.ReadLine();
            input = input == null ? "" : input;
            return new StringValue(input);
        }
    }
}
