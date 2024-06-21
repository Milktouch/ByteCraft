using ByteCraft.Data;
using ByteCraft.Exceptions;
using ByteCraft.Scopes;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using ByteCraft;

namespace ByteCraft.Parsing
{
    internal static class Parser
    {
        // Parameter and Value Regex
        public static readonly Regex NumberRegex = new Regex(@"^[+-]?(?=\.\d|\d)(?:\d+)?(?:\.?\d*)(?:[eE][+-]?\d+)?$",RegexOptions.Compiled);
        public static readonly Regex VariableNameRegex = new Regex(@"^\s*(?!true\s*$|True\s*$|false\s*$|False\s*$)([a-zA-Z_][a-zA-Z0-9_]*)\s*$", RegexOptions.Compiled);
        public static readonly Regex StringRegex = new Regex("""^\s*"((?:[^"\\]|(?:\\\\)|(?:\\\\)*\\.{1})*)"\s*$""", RegexOptions.Compiled);
        public static readonly Regex BooleanRegex = new Regex(@"^\s*(true|false|True|False)\s*$", RegexOptions.Compiled);

        //variable assignment
        public static readonly Regex VariableAssignmentRegex = new Regex(@"^([a-zA-Z_][a-zA-Z0-9_]*)\s*=\s*(.+?)\s*$", RegexOptions.Compiled);
        //operation with arguments
        public static readonly Regex OperationWithArgumentsRegex = new Regex(@"^([a-zA-Z_][a-zA-Z0-9_]*)\s+(.*?)\s*$", RegexOptions.Compiled);
        //operation without arguments
        public static readonly Regex OperationWithoutArgumentsRegex = new Regex(@"^([a-zA-Z_][a-zA-Z0-9_]*)\s*$", RegexOptions.Compiled);

        //special actions
        //import 
        public static readonly Regex ImportRegex = new Regex(@"^@import\s+(.+?)\s*$");


        public static class SectionParser
        {
            internal static CodeLine ParseSection(string line)
            {
                CodeLine codeLine = new CodeLine(LineType.Section);
                //switch on the first word after the #
                switch (line.Split(' ')[0].Substring(1))
                {

                }
                return codeLine;
            }
        }
        public static class SpecialActionParser
        {
            internal static CodeLine ParseSpecialAction(string line)
            {
                
                //switch on the first word after the @
                if(ImportRegex.IsMatch(line))
                {
                    Match match = ImportRegex.Match(line);
                    ImportLine codeLine = new ImportLine(match.Groups[1].Value);
                    return codeLine;
                }
                throw new RuntimeError($"""Special action "{line}" is not a valid special action""");
            }
        }
        public static class OperationParser
        {
            internal static CodeLine ParseOperation(string line)
            {

                if (OperationWithArgumentsRegex.IsMatch(line))
                {
                    Match match = OperationWithArgumentsRegex.Match(line);
                    List<Value> arguments = ParseArguments(match.Groups[2].Value);
                    String operationName = match.Groups[1].Value;
                    OperationLine codeLine = new OperationLine(operationName, arguments);
                    return codeLine;
                }
                if (OperationWithoutArgumentsRegex.IsMatch(line))
                {
                    OperationLine codeLine = new OperationLine(line, new List<Value>());
                    return codeLine;
                }
                throw new RuntimeError($"""Operation "{line}" is not a valid operation""");
            }
            internal static List<Value> ParseArguments(string arguments)
            {
                List<string> args = SplitByCharNotInQuotes(arguments, ',');
                List<Value> values = new List<Value>();
                foreach (string arg in args)
                {
                    values.Add(VariableParser.ParseValue(arg.Trim()));
                }
                return values;
            }

        }
        internal static CodeLine ParseLine(string line)
        {
            line = line.Trim();
            CodeLine codeLine;
            if (line.StartsWith("#"))
            {
                codeLine=SectionParser.ParseSection(line);
            }
            else if (line.StartsWith("@"))
            {
                codeLine=SpecialActionParser.ParseSpecialAction(line);
            }
            else if (VariableAssignmentRegex.IsMatch(line))
            {
                codeLine=VariableParser.ParseVariable(line);
            }
            else
            {
                codeLine = OperationParser.ParseOperation(line);
            }
            return codeLine;
        }
        public static class VariableParser
        {
            // <variable_name> = <value>
            internal static CodeLine ParseVariable(string line)
            {
                Match match = VariableAssignmentRegex.Match(line);
                string variableName = match.Groups[1].Value;
                if (!VariableNameRegex.IsMatch(variableName))
                {
                    throw new RuntimeError($"Variable name ({variableName}) is not a valid variable name");
                }
                string value = match.Groups[2].Value;
                return new VariableLine(variableName, ParseValue(value));
            }

            internal static Value ParseValue(string value)
            {
                value = value.Trim();
                if (value == "null")
                {
                    return new NullValue();
                }
                if (NumberRegex.IsMatch(value))
                {
                    decimal number = decimal.Parse(value,NumberStyles.Any);
                    return new NumberValue(number);
                }
                if (VariableNameRegex.IsMatch(value))
                {
                    Variable var = Interpreter.currentThread.scopeStack.GetVariable(value);
                    return var.GetValue();
                }
                if (StringRegex.IsMatch(value))
                {
                    string str = StringRegex.Match(value).Groups[1].Value;
                    // Replace escaped characters
                    // Replace \" with "
                    // Replace \\ with \
                    return new StringValue(str.Replace("\\\"", "\"").Replace("\\\\", "\\"));
                }
                if (BooleanRegex.IsMatch(value))
                {
                    bool boolean = Boolean.Parse(value.ToLower());
                    return new BooleanValue(boolean);
                }
                throw new RuntimeError($"Value ({value}) is not a valid value");

            }
        }

        
        
        internal static List<string> SplitByCharNotInQuotes(string input, char splitChar)
        {
            List<string> split = new List<string>();
            int start = 0;
            bool inQuotes = false;
            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] == splitChar && !inQuotes )
                {
                    split.Add(input.Substring(start, i - start));
                    start = i + 1;
                }
                else
                {
                    if (input[i] == '"' )
                    {
                        if (!inQuotes)
                        {
                            inQuotes = true;
                        }
                        else
                        {
                            if (!IsEscaped(input,i))
                            {
                                inQuotes = false;
                            }
                        }
                    }
                
                }
            }
            split.Add(input.Substring(start));
            return split;
        }
        internal static bool IsEscaped(string input, int index)
        {
            bool isEscaped = false;
            for (int i = index - 1; i >= 0; i--)
            {
                if (input[i] == '\\')
                {
                    isEscaped = !isEscaped;
                }
                else
                {
                    break;
                }
            }
            return isEscaped;
        }
    }


}