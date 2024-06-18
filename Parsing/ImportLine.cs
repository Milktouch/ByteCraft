namespace ByteCraft.Parsing;

internal class ImportLine : CodeLine
{
    public string filename { get; private set; }
    public ImportLine(string filename) : base(LineType.Import)
    {
        this.filename = filename;
    }
    public override string ToString()
    {
        return $"import {filename}";
    }
}