using ByteCraft;
using ByteCraft.BasicOperations;
using ByteCraft.BuiltInOperations;
using ByteCraft.Data;
using ByteCraft.Operations;
using ByteCraft.Parsing;



string fileName = "/Users/milktouch/Documents/GitHub/ByteCraft/Project Structure/program.bct";
CodeFile codeFile = new CodeFile(new FileInfo(fileName));

OperationDefinition write = OperationDefinition.GetOperationDefinitionFromType(typeof(Write));
OperationDefinition read = OperationDefinition.GetOperationDefinitionFromType(typeof(Read));
OperationDefinition length = OperationDefinition.GetOperationDefinitionFromType(typeof(LenOp));
OperationDefinition indexOf = OperationDefinition.GetOperationDefinitionFromType(typeof(IndexOf));
codeFile.AddOperation(write);
codeFile.AddOperation(read);
codeFile.AddOperation(length);
codeFile.AddOperation(indexOf);


Interpreter interpreter = new Interpreter(codeFile);
interpreter.Start();



