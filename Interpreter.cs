using ByteCraft.Program;
using ByteCraft.Data;
using ByteCraft;
using ByteCraft.Exceptions;
using ByteCraft.Operations;
using ByteCraft.Scopes;
using ByteCraft.Parsing;
namespace ByteCraft
{
    internal class Interpreter
    {
        public static Interpreter currentThread { get; private set; }
        internal readonly ScopeStack scopeStack;
        private CodeFile currentFile;
        private int currentLine;
        public Interpreter(CodeFile currentFile)
        {
            scopeStack = new ScopeStack(currentFile);
            currentLine = 0;
            this.currentFile = currentFile;
        }

        public void Start()
        {
            currentThread = this;
            for (int i = currentLine; i < currentFile.lines.Length; i++)
            {
                string line = currentFile.lines[i];
                if (line=="")
                {
                    continue;
                }
                CodeLine codeLine = Parser.ParseLine(line);
                ExecuteOperation(codeLine);
            }
        }

        private void ExecuteOperation(CodeLine line)
        {
            switch (line.lineType)
            {
                case LineType.Operation:
                    OperationLine opLine = line as OperationLine;
                    string opName = opLine.operationName;
                    OperationDefinition op = currentFile.GetOperationDefinition(opName);
                    if (op==null)
                    {
                        throw new RuntimeError("Operation not found: "+opName,line.lineNumber,currentFile.lines[currentLine]);
                    }
                    Value res =op.ExecuteOperation(opLine.arguments.ToArray());
                    scopeStack.currentScope.Result.SetValue(res);
                    break;
                case LineType.VariableAssignment:
                    VariableLine varLine = line as VariableLine;
                    string varName = varLine.variableName;
                    Value varValue = varLine.value;
                    Variable var = scopeStack.GetVariable(varName);
                    var.SetValue(varValue);
                    scopeStack.currentScope.Result.SetValue(varValue);
                    break;
            }
        }

        public void ApplyScope(Scope scope)
        {
            this.currentLine = (int)scope.line;
            this.currentFile = scope.file;
        }

    }
        
}

