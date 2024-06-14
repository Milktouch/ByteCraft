

using ByteCraft;
using ByteCraft.BasicOperations;
using ByteCraft.Data;
using ByteCraft.Data.Memory;
using ByteCraft.Operations;
using ByteCraft.Scopes;


Variable var = new Variable("msg");
var.SetValue(new StringValue("enter your birthday"));

OperationDefinition? writeOp = OperationDefinition.GetOperationDefinitionFromType(typeof(Write));
OperationDefinition? inputOp = OperationDefinition.GetOperationDefinitionFromType(typeof(Input));


Value result = writeOp.ExecuteOperation(new Value[] { var.GetValue() });

result = inputOp.ExecuteOperation(new Value[] {});

var.SetValue(result);

result = writeOp.ExecuteOperation(new Value[] { var.GetValue() });



