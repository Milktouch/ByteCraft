namespace ByteCraft.Parsing;

internal class SubExpression : Token
{
    public readonly Expression expression;
    public SubExpression(string stringValue) : base(Token.Type.SubExpression, stringValue)
    {
        expression = new Expression(stringValue);
        expression.Parse();
    }
    
    public override string ToString()
    {
        string str = "";
        foreach (Token token in expression.Tokens)
        {
            str += token.ToString()+ " ";
        }

        return str;
    }
}