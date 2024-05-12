public abstract class IOperation
{
    public string name{get;}
    public string returnType{get;}
    public List<string> parameterTypes{get;}
    public abstract Variable Execute(List<Variable> parameters);


}