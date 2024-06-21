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
    public void ExitScope()
    {
        Scope prev = scopeStack.Pop();
        switch (currentScope.scopeType)
        {
            case ScopeType.File:
                if (scopeStack.Count == 0)
                {
                    //end program
                }
                break;
            case ScopeType.Function:
                currentScope.Result.SetValue(prev.Result.GetValue());
                break;
            case ScopeType.If:
                break;
            case ScopeType.Loop:
                break;
        }
    }
    public void NewFileScope(string filename)
    {
        Scope s = FileScope.GetFileScope(filename);
        scopeStack.Push(s);
    }
    public Variable GetVariable(string name)
    {
        //TODO: check the variable is not a saved keyword (and,or,Result)
        Variable? v = currentScope.GetVariable(name);
        if (v == null)
        {
            v = new Variable(name);
            currentScope.SetVariable(v);
        }
        return v;
    }
    
}