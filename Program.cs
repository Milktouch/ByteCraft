using ByteCraft.Data;
using ByteCraft.Variables;


// Create a new variable
NumberValue val = new NumberValue(100);
ValueVariable var = new ValueVariable("myvar",val);

Variable referenceVar = new ReferenceVariable("myref",var);


Console.WriteLine(var.GetValue().value.ToString());
Console.WriteLine(referenceVar.GetValue().value.ToString());
var.SetValue(new NumberValue(200));
Console.WriteLine(var.GetValue().value.ToString());
Console.WriteLine(referenceVar.GetValue().value.ToString());