
namespace ByteCraft
{
    internal class Line
    {
        public readonly string text;
        public readonly int lineNumber;
        public Line(string text, int lineNumber)
        {
            this.text = text;
            this.lineNumber = lineNumber;
        }
    }
}