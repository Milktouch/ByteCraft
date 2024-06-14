using ByteCraft.Data;

namespace ByteCraft.Operations
{
    public abstract class Operation
    {
        public Value[] arguments { get;private set; } = new Value[0];
        /// <summary>
        ///  This method returns the argument associated with the provided index
        /// </summary>
        /// <param name="index"> The argument index (starts at 0)</param>
        /// <returns>The argument associated with the index as a Value object. If the index is outside the bounds this method will return null</returns>
        public Value? GetArgument(int index)
        {
            if(index < 0 || index >= arguments.Length)
            {
                return null;
            }
            return arguments[index];
        }
        internal void SetArguments(Value[] args)
        {
            if (args==null)
            {
                arguments = new Value[0];
            }
            else
            {
                arguments = args;
            }
        }
        public abstract Value Execute();
    }
}
