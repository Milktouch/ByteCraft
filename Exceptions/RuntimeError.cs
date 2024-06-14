namespace ByteCraft.Exceptions{
    public class RuntimeError : Exception
    {
        public int line { get; private set; } = - 1 ;
        public string lineText { get; private set; } = "";
        public RuntimeError(string message, int line, string lineText) : base(message)
        {
            this.line = line;
            this.lineText = lineText;
        }
        public RuntimeError(string message) : base(message)
        {
        }
        public override string ToString()
        {
            return $"RuntimeError: {Message} at line {line}:\n{lineText}";
        }
        public void Print()
        {
            Console.WriteLine(ToString());
        }
    }
}

