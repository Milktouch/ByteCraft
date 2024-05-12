using System.Dynamic;

public abstract class StaticVariable{
    public readonly string name;
    public readonly string type;

    public StaticVariable(string name, string type) {
        this.name = name;
        this.type = type;
    }   

    public abstract void SetValue(Object value);
    public abstract Object GetValue();

}
public class Variable
{
    public readonly string name;
    public string type{get;private set;}
    public dynamic value{get;private set;}
    public Variable(string name, string type)
    {
        this.name = name;
        this.type = type;
    }
    public void SetValue(dynamic value,string type)
    {
        this.value = value;
        this.type = type;
    }
    public Variable Copy()
    {
        Variable copy = new Variable(name,type);
        copy.SetValue(value,type);
        return copy;
    }

}
