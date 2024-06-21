using ByteCraft;
using ByteCraft.BasicOperations;
using ByteCraft.Data;
using ByteCraft.Operations;
using ByteCraft.Parsing;






string fileName = "/Users/milktouch/Documents/GitHub/ByteCraft/Project Structure/program.bct";
CodeFile codeFile = new CodeFile(new FileInfo(fileName));

OperationDefinition write = OperationDefinition.GetOperationDefinitionFromType(typeof(Write));
OperationDefinition read = OperationDefinition.GetOperationDefinitionFromType(typeof(Input));
codeFile.AddOperation(write);
codeFile.AddOperation(read);


Interpreter interpreter = new Interpreter(codeFile);
interpreter.Start();


