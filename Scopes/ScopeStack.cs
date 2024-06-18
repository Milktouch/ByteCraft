namespace ByteCraft.Scopes;

internal class ScopeStack
{
    private Stack<Scope> scopeStack = new();
    public Scope currentScope
    {
        get
        {
            return scopeStack.Peek();
        }
    }
    public ScopeStack(CodeFile file)
    {
        Scope s = new(file,null);
        scopeStack.Push(s);
    }
    public void CreateSubScope()
    {
        Scope s = new(currentScope.file,currentScope);
        scopeStack.Push(s);
    }
    public void PopScope()
    {
        scopeStack.Pop();
    }
    public void NewFileScope(string filename)
    {
        Scope s = FileScope.GetFileScope(filename);
        scopeStack.Push(s);
    }
    public Variable GetVariable(string name)
    {
        Variable? v = currentScope.GetVariable(name);
        if (v == null)
        {
            v = new Variable(name);
            currentScope.SetVariable(v);
        }
        return v;
    }
}