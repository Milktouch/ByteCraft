using ByteCraft.Data;

namespace ByteCraft.Parsing;

internal class Token
{
    public enum Type
    {
        SubExpression,
        RawValue,
        Variable,
        Operation,
        Operator,
        IndexerStart,
        IndexerEnd,
        SubExpressionStart,
        SubExpressionEnd,
        ValueGroupStart,
        ValueGroupEnd,
        Separator,
        Assignment,
        Import,
        
    }
    public readonly Type type;
    public readonly string stringValue;
    public readonly Value value;
    public Token(Type type, string stringValue)
    {
        this.type = type;
        this.stringValue = stringValue;
    }
    public Token(Value value,string stringValue)
    {
        this.value = value;
        this.stringValue = stringValue;
        this.type = Type.RawValue;
    }
    public override string ToString()
    {
        return $"< {type}, {stringValue} >";
    }
    
}