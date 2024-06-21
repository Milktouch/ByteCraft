using ByteCraft.Data;

namespace ByteCraft.Parsing;

internal class ExpressionResult
{
    private Exception exception;
    public bool IsError => exception != null;
    public Exception Exception => exception;
    public Value Value { get; private set; }
}