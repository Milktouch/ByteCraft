

namespace ByteCraft.Parsing
{
    internal enum LineType
    {
        VariableAssignment,
        Operation,
        SpecialAction,
        Import,
        Section
    }
    internal class CodeLine
    {
        public CodeFile file { get; internal set; }
        public int lineNumber { get; internal set; }
        public readonly LineType lineType;

        public CodeLine(LineType type)
        {
            lineType = type;
        }

    }
}
