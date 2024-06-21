using System.Text;
using System.Text.RegularExpressions;
using ByteCraft.Data;
using ByteCraft.Exceptions;

namespace ByteCraft.Parsing;

internal class Expression
{
    private static readonly Regex NumberRegex = new("""^(?=\.\d|\d)(?:\d+)?(?:\.?\d*)(?:[eE][+-]?\d+)?""", RegexOptions.Compiled);
    private static readonly Regex VariableNameRegex = new("""^[a-zA-Z_][a-zA-Z0-9_]*""", RegexOptions.Compiled);
    private static readonly Regex BooleanRegex = new("""^(true|false)(?![0-9A-Za-z_])""", RegexOptions.Compiled);
    private static readonly Regex InEqualityRegex = new("""^(==|!=|<=|>=|<|>)""", RegexOptions.Compiled);
    private static readonly Regex OperationRegex = new("""^([a-zA-Z][a-zA-Z0-9_]*)\s*{(.*)}""", RegexOptions.Compiled);
    private static readonly Regex IndexedVariableRegex = new("""^([a-zA-Z_][a-zA-Z0-9_]*)\s*\[.*\]""", RegexOptions.Compiled);
    
    public readonly string expression;
    private readonly List<Token> tokens = new();
    public List<Token> Tokens => tokens;
    public Expression(string expression)
    {
        this.expression = expression.TrimStart();
    }
    
    public void Parse()
    {
        if (expression.Length==0)
        {
            return;
        }
        char c = expression[0];
        if (c == '#')
        {
            //TODO: ParseSection();
            //sections include: functions , if , else , while , for
        }
        else if (c == '@')
        {
            //TODO: ParseSpecialAction();
            //special actions include: import 
        }
        else
        {
            ParseExpression();
        }
    }
    private void ParseExpression()
    {
        bool inQuotes = false;
        string strValue = "";
        int i = 0;
        for ( ; i < this.expression.Length; i++)
        {
            string newExpression = this.expression.Substring(i);
            char c = expression[i];
            //quote tracking
            if (c=='"')
            {
                if (!inQuotes)
                {
                    inQuotes = true;
                    continue;
                }
                else
                {
                    //check if the quote is escaped
                    //the function IsEscaped returns true if the quote is escaped
                    //and false if it is not
                    //if the qoute is escaped it means we are still in qoutes
                    //if it is not escaped it means we are out of quotes
                    inQuotes = IsEscaped(expression, i);
                    if (inQuotes)
                    {
                        strValue += c;
                    }
                    else
                    {
                        tokens.Add(new Token(new StringValue(strValue), '"'+strValue+'"'));
                        strValue = "";
                    }
                    continue;
                }
            }
            if (inQuotes)
            {
                strValue += c;
                continue;
            }
            if (c=='(')
            {
                tokens.Add(new Token(Token.Type.SubExpressionStart, "("));
                int closingBracketIndex = GetMatchingClosingBracket(i);
                string subExpression = expression.Substring(i + 1, closingBracketIndex - i - 1);
                tokens.Add(new SubExpression(subExpression));
                i = closingBracketIndex;
                tokens.Add(new Token(Token.Type.SubExpressionEnd, ")"));
                continue;
            }
            if (c=='[')
            {
                tokens.Add(new Token(Token.Type.IndexerStart, "["));
                int closingBracketIndex = GetMatchingClosingBracket(i);
                string subExpression = expression.Substring(i + 1, closingBracketIndex - i - 1);
                tokens.Add(new SubExpression(subExpression));
                i = closingBracketIndex;
                tokens.Add(new Token(Token.Type.IndexerEnd, "]"));
                continue;
            }
            if (c=='{')
            {
                tokens.Add(new Token(Token.Type.Operation, "{"));
                int closingBracketIndex = GetMatchingClosingBracket(i);
                string subExpression = expression.Substring(i + 1, closingBracketIndex - i - 1);
                tokens.Add(new SubExpression( subExpression));
                i = closingBracketIndex;
                tokens.Add(new Token(Token.Type.ValueGroupEnd, "}"));
                continue;
            }
            if (c==',')
            {
                tokens.Add(new Token(Token.Type.Separator, ","));
                continue;
            }
            if (NumberRegex.IsMatch(newExpression))
            {
                Match match = NumberRegex.Match(newExpression);
                string number = match.Value;
                tokens.Add(new Token(Parser.VariableParser.ParseValue(number), number));
                i += number.Length - 1;
                continue;
            }
            if (OperationRegex.IsMatch(newExpression))
            {
                Match match = OperationRegex.Match(newExpression);
                string operation = match.Groups[1].Value;
                string arguments = match.Groups[2].Value;
                tokens.Add(new Token(Token.Type.Operation, operation));
                tokens.Add(new Token(Token.Type.ValueGroupStart, "{"));
                tokens.Add(new SubExpression(arguments));
                tokens.Add(new Token(Token.Type.ValueGroupEnd, "}"));
                i += operation.Length + arguments.Length + 2;
                continue;
            }
            if (BooleanRegex.IsMatch(newExpression))
            {
                Match match = BooleanRegex.Match(newExpression);
                string boolean = match.Value;
                tokens.Add(new Token(Parser.VariableParser.ParseValue(boolean), boolean));
                i += boolean.Length - 1;
                continue;
            }
            if (VariableNameRegex.IsMatch(newExpression))
            {
                Match match = VariableNameRegex.Match(newExpression);
                string name = match.Value;
                tokens.Add(new Token(Token.Type.Variable, name));
                i += name.Length - 1;
                continue;
            }
            if (InEqualityRegex.IsMatch(newExpression))
            {
                Match match = InEqualityRegex.Match(newExpression);
                string op = match.Value;
                tokens.Add(new Token(Token.Type.Operator, op));
                i += op.Length - 1;
                continue;
            }
            if (IsOperator(c))
            {
                tokens.Add(new Token(Token.Type.Operator, c.ToString()));
                continue;
            }
            if (char.IsWhiteSpace(c))
            {
                continue;
            }
            if (c == '=')
            {
                tokens.Add(new Token(Token.Type.Assignment,"="));
                continue;
            }
            
            throw new RuntimeError($"Invalid character {c} at index {i}");
        }
    }
    
    private int GetMatchingClosingBracket(int openingBracketIndex)
    {
        char openingBracket = expression[openingBracketIndex];
        char closingBracket = openingBracket switch
        {
            '(' => ')',
            '[' => ']',
            '{' => '}',
            _ => throw new SyntaxError("Invalid opening bracket")
        };
        int bracketCount = 1;
        bool inQuotes = false;
        iteratorLoop : for (int i = openingBracketIndex + 1; i < this.expression.Length; i++)
        {
            char c= expression[i];
            //quote tracking
            if (c=='"')
            {
                if (!inQuotes)
                {
                    inQuotes = true;
                    continue;
                }
                else
                {
                    //check if the quote is escaped
                    //the function IsEscaped returns true if the quote is escaped
                    //and false if it is not
                    //if the qoute is escaped it means we are still in qoutes
                    //if it is not escaped it means we are out of quotes
                    inQuotes = IsEscaped(expression, i);
                    continue;
                }
            }
            if (inQuotes)
            {
                continue;
            }
            if (c==openingBracket)
            {
                bracketCount++;
            }
            else if (c==closingBracket)
            {
                bracketCount--;
                if (bracketCount==0)
                {
                    return i;
                }
            }
        }
        throw new RuntimeError("No matching closing bracket found");
    }
    
    private static bool IsOperator(char c)
    {
        return c=='+' || c=='-' || c=='*' || c=='/' || c=='%';
    }
    private static bool IsEscaped(string input, int index)
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
    
    public override string ToString()
    {
        StringBuilder sb = new();
        foreach (Token token in tokens)
        {
            sb.Append(token+" ");
        }
        return sb.ToString();
    }
    
    
    
}