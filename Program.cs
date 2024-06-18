using ByteCraft;
using ByteCraft.BasicOperations;
using ByteCraft.Operations;

string fileName = "/Users/milktouch/Documents/GitHub/ByteCraft/Project Structure/program.bct";
CodeFile codeFile = new CodeFile(new FileInfo(fileName));

OperationDefinition write = OperationDefinition.GetOperationDefinitionFromType(typeof(Write));
OperationDefinition read = OperationDefinition.GetOperationDefinitionFromType(typeof(Input));
OperationDefinition add = OperationDefinition.GetOperationDefinitionFromType(typeof(Add));
OperationDefinition parseInt = OperationDefinition.GetOperationDefinitionFromType(typeof(ParseInt));
codeFile.AddOperation(write);
codeFile.AddOperation(read);
codeFile.AddOperation(add);
codeFile.AddOperation(parseInt);

Interpreter interpreter = new Interpreter(codeFile);
interpreter.Start();
