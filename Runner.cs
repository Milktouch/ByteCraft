
namespace ByteCarft
{
    public class Runner
    {
        public readonly Runtime runtime;
        public readonly int currentLine;
        public readonly List<string> code;

        public Runner(List<string> code)
        {
            this.code = code;
            this.runtime = new Runtime();
        }


    }
}