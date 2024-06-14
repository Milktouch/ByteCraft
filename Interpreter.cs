using ByteCraft.Program;
using ByteCraft.Data;
using ByteCraft;
using ByteCraft.Scopes;
using ByteCraft.Parsing;
namespace ByteCarft
{
    internal class Interpreter
    {

        private readonly Stack<Scope> scopeStack = new();
        private Scope currentScope { 
            get
            {
                return scopeStack.Peek();
            }
        }
        private CodeFile currentFile;
        private long currentLine;
        public Interpreter(CodeFile currentFile)
        {
            Scope s = new Scope(currentFile, 0);
            scopeStack.Push(s);
            currentLine = 0;
            this.currentFile = currentFile;
        }

        public void Start()
        {

        }

        private void ExecuteOperation(CodeLine line)
        {
            
        }

        public void ApplyScope(Scope scope)
        {
            this.currentLine = scope.line;
            this.currentFile = scope.file;
        }

    }
        
}

