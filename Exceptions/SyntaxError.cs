namespace ByteCraft.Exceptions{
    public class SyntaxError : Exception
    {
        public readonly int line;
        public readonly string lineText;
        public SyntaxError(string message, int line, string lineText) : base(message)
        {
            this.line = line;
            this.lineText = lineText;
        }
        public override string ToString()
        {
            return $"SyntaxError: {Message} at line {line}:\n{lineText}";
        }
        public void Print()
        {
            Console.WriteLine(ToString());
        }
    }
}


