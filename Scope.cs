
using ByteCraft.Variables;

namespace ByteCarft
{
    public class Scope
    {
        public readonly Runtime runtime;
        public readonly Scope fileScope;
        public readonly List<Variable> variables = new();
        public readonly List<Line> lines = new();
        public Scope(Runtime runtime)
        {
            this.runtime = runtime;
        }
    }
}
