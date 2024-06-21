namespace ByteCraft.Exceptions{
    public class SyntaxError : Exception
    {
        public int line { get; internal set; } = - 1 ;
        public string lineText { get; internal set; } = "";
        public SyntaxError(string message, int line, string lineText) : base(message)
        {
            this.line = line;
            this.lineText = lineText;
        }
        
        public SyntaxError(string message) : base(message)
        {
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


