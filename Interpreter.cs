using ByteCraft.Program;
using ByteCraft.Data;
using ByteCraft;
using ByteCraft.Data.Arithmetic;
using ByteCraft.Data.OtherQualities;
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
            for (;currentLine < currentFile.lines.Length; currentLine++)
            {
                string line = currentFile.lines[currentLine];
                if (string.IsNullOrWhiteSpace(line))
                {
                    continue;
                }
                try
                {
                    ExecuteLine(line);
                }
                catch (RuntimeError e)
                {
                    throw new RuntimeError(e.Message,currentLine,line);
                }
            }
            
        }
        public Value ExecuteLine(string line)
        {
            Expression exp = new Expression(line);
            exp.Parse();
            List<Token> tokens = exp.Tokens;
            //check token combinations to analyze the line
            if (tokens.Count >= 2)
            {
                Token t1 = tokens[0];
                Token t2 = tokens[1];
                if (t1.type == Token.Type.Variable && t2.type == Token.Type.Assignment)
                {
                    Variable var = scopeStack.GetVariable(t1.stringValue);
                    Value value = EvaluateExpression(tokens.GetRange(2, tokens.Count - 2));
                    var.SetValue(value);
                    return value;
                }

                if (tokens.Count >= 5)
                {
                    Token t3 = tokens[2];
                    Token t4 = tokens[3];
                    Token t5 = tokens[4];
                    if (t1.type == Token.Type.Variable && t2.type == Token.Type.IndexerStart && t3.type == Token.Type.SubExpression && t4.type == Token.Type.IndexerEnd && t5.type == Token.Type.Assignment)
                    {
                        Variable var = scopeStack.GetVariable(t1.stringValue);
                        SubExpression sub = t3 as SubExpression;
                        Value index = EvaluateExpression(sub.expression.Tokens);
                        Value newValue = EvaluateExpression(tokens.GetRange(5, tokens.Count - 5));
                        if (var.GetValue() is Indexable)
                        {
                            Indexable indexable = var.GetValue() as Indexable;
                            if (index is NumberValue)
                            {
                                NumberValue number = index as NumberValue;
                                indexable.SetValueAt(number, newValue);
                                return newValue;
                            }
                            else
                            {
                                throw new RuntimeError("Index must be a number");
                            }
                        }
                        else if (var.GetValue() is Keyable)
                        {
                            Keyable keyable = var.GetValue() as Keyable;
                            keyable.SetValue(index, newValue);
                            return newValue;
                        }
                        else
                        {
                            throw new RuntimeError($"value of type {var.GetValue().type} is not indexable nor keyable");
                        }
                    }
                        
                }
            }

            return EvaluateExpression(tokens);
        }
        internal List<Token> ReplaceTokensWithValues(List<Token> tokens)
        {
            List<Token> newTokens = new List<Token>();
            for (int i = 0; i < tokens.Count; i++)
            {
                Token token = tokens[i];
                if (token.type == Token.Type.Variable)
                {
                    string varName = token.stringValue;
                    Variable var = scopeStack.GetVariable(varName);
                    newTokens.Add(new Token(var.GetValue(), var.GetValue().ToString()));
                    continue;
                }
                if (token.type== Token.Type.Operation)
                {
                    string opName = token.stringValue;
                    SubExpression sub = tokens[i + 2] as SubExpression;
                    List<Value> subValues = ParseValueGroup(sub.expression.Tokens);
                    Value result = ExecuteOperation(opName, subValues);
                    newTokens.Add(new Token(result, result.ToString()));
                    i += 3;
                    continue;
                }
                newTokens.Add(token);
            }
            newTokens = EvaluateIndexers(newTokens);
            return newTokens;
        }

        internal List<Token> EvaluateIndexers(List<Token> tokens)
        {
            List<Token> newTokens = new List<Token>();
            if(tokens.Count == 0)
            {
                return newTokens;
            }
            newTokens.Add(tokens[0]);
            for (int i = 1; i < tokens.Count; i++)
            {
                Token token = tokens[i];
                if (token.type == Token.Type.IndexerStart&& i!=0&&tokens[i-1].type == Token.Type.RawValue)
                {
                    Token previousToken = tokens[i - 1];
                    SubExpression indexedVariable = tokens[i  +1] as SubExpression;
                    Value index = EvaluateExpression(indexedVariable.expression.Tokens);
                    if (previousToken.value is Indexable)
                    {
                        Indexable indexable = previousToken.value as Indexable;
                        if (index is NumberValue)
                        {
                            NumberValue number = index as NumberValue;
                            Value result = indexable.GetValueAt(number);
                            newTokens.Add(new Token(result, result.ToString()));
                            i+=2;
                        }
                        else
                        {
                            throw new RuntimeError("Index must be a number");
                        }
                    }
                    else if (previousToken.value is Keyable)
                    {
                        Keyable keyable = previousToken.value as Keyable;
                        Value result = keyable.GetValue(index);
                        newTokens.Add(new Token(result, result.ToString()));
                        i+=2;
                    }
                    else
                    {
                        throw new RuntimeError($"value of type {previousToken.value.type} is not indexable or keyable");
                    }
                    newTokens.RemoveAt(newTokens.Count-2);
                    continue;
                }
                newTokens.Add(token);
            }

            return newTokens;
        }
        internal Value EvaluateExpression(List<Token> tokens)
        {
            tokens = ReplaceTokensWithValues(tokens);
            if(tokens.Count == 1)
            {
                return tokens[0].value;
            }
            //look for sub expressions to evaluate first
            for (int i = 0; i < tokens.Count; i++)
            {
                Token token = tokens[i];
                if (token.type == Token.Type.SubExpression)
                {
                    SubExpression sub = token as SubExpression;
                    Value result = EvaluateExpression(sub.expression.Tokens);
                    tokens[i] = new Token(result, result.ToString());
                    tokens.RemoveAt(i-1);
                    tokens.RemoveAt(i);
                    i--;
                }
            }
            // look for multiplication , division and modulo operations to do first
            for (int i = 1; i < tokens.Count; i++)
            {
                Token token = tokens[i];
                if (token.type == Token.Type.Operator)
                {
                    if (token.stringValue == "*" || token.stringValue == "/" || token.stringValue == "%")
                    {
                        char op = token.stringValue[0];
                        Value left = tokens[i - 1].value;
                        Value right = tokens[i + 1].value;
                        Value result = null;
                        try
                        {
                            switch (op)
                            {
                                case '*':
                                    Multiplication multiplication = left as Multiplication;
                                    result= multiplication.Multiply(right);
                                    break;
                                case '/':
                                    Division division = left as Division;
                                    result = division.Divide(right);
                                    break;
                                case '%':
                                    Modulo modulo = left as Modulo;
                                    result = modulo.Modulo(right);
                                    break;
                            }
                        }
                        catch (System.InvalidCastException)
                        {
                            throw new RuntimeError($"Operation {op} is not supported by {left.type}");
                        }
                        tokens[i - 1] = new Token(result, result.ToString());
                        tokens.RemoveAt(i);
                        tokens.RemoveAt(i);
                        i--;
                    }
                }
            }
            // look for addition and subtraction operations to do next
            for (int i = 1; i < tokens.Count; i++)
            {
                Token token = tokens[i];
                if (token.type == Token.Type.Operator)
                {
                    if (token.stringValue == "+" || token.stringValue == "-")
                    {
                        char op = token.stringValue[0];
                        Value left = tokens[i - 1].value;
                        Value right = tokens[i + 1].value;
                        Value result = null;
                        try
                        {
                            switch (op)
                            {
                                case '+':
                                    Addition addition = left as Addition;
                                    result = addition.Add(right);
                                    break;
                                case '-':
                                    Subtraction subtraction = left as Subtraction;
                                    result = subtraction.Subtract(right);
                                    break;
                            }
                        }
                        catch (System.InvalidCastException)
                        {
                            throw new RuntimeError($"Operation {op} is not supported by {left.type}");
                        }
                        tokens[i - 1] = new Token(result, result.ToString());
                        tokens.RemoveAt(i);
                        tokens.RemoveAt(i);
                        i--;
                    }
                }
            }
            if (tokens.Count == 1)
            {
                return tokens[0].value;
            }
            throw new RuntimeError("Invalid expression");
        }
        //a value group is a group of values that are separated by , and contained in {}
        internal List<Value> ParseValueGroup(List<Token> tokens)
        {
            List<Value> values = new List<Value>();
            List<Token> currentGroup = new List<Token>();
            for (int i = 0; i < tokens.Count; i++)
            {
                Token token = tokens[i];
                if (token.type == Token.Type.Separator)
                {
                    values.Add(EvaluateExpression(currentGroup));
                    currentGroup.Clear();
                }
                else
                {
                    currentGroup.Add(token);
                }
            }
            if (currentGroup.Count != 0)
            {
                values.Add(EvaluateExpression(currentGroup));
            }
            return values;
        }
        private Value ExecuteOperation(string operationName, List<Value> arguments)
        {
            Value operationVal = scopeStack.GetVariable(operationName).GetValue();
            if (!operationVal.CanBeCastTo<OperationValue>())
            {
                throw new RuntimeError($"{operationName} is not an operation");
            }
            OperationValue operation = operationVal.As<OperationValue>();
            return operation.ExecuteOperation(arguments.ToArray());
        }
        public void ApplyScope(Scope scope)
        {
            this.currentLine = (int)scope.line;
            this.currentFile = scope.file;
        }

    }
        
}

