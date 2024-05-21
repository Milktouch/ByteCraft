using System.Text.RegularExpressions;

namespace ByteCarft
{
    public class Parser
    {
        public static readonly Regex NumberRegex = new Regex(@"[+-]?(?=\.\d|\d)(?:\d+)?(?:\.?\d*)(?:[eE][+-]?\d+)?");
        public static readonly Regex VariableNameRegex = new Regex(@"^[a-zA-Z_]+([a-zA-Z0-9_]*)");
        public static readonly Regex StringRegex = new Regex("""("(?:[^"\\]|(?:\\\\)|(?:\\\\)*\\.{1})*")""");
        public static readonly Regex BooleanRegex = new Regex(@"^(true|false|True|False)$");
    }
}