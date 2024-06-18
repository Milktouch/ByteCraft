using ByteCraft.Data;

namespace ByteCraft.Parsing;

internal class VariableLine : CodeLine
{
    public readonly string variableName;
    public readonly Value value;
    public VariableLine(string name, Value value) : base(LineType.VariableAssignment)
    {
        this.variableName = name;
        this.value = value;
    }
}