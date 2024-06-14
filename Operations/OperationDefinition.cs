using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ByteCraft.Data;

namespace ByteCraft.Operations
{
    internal class OperationDefinition
    {

        public readonly string opName;
        public readonly string opDescription;
        public readonly ConstructorInfo constructorInfo;
        public OperationDefinition(OperationAttribute metadata,ConstructorInfo constructor)
        {
            this.opName = metadata.Name;
            this.opDescription = metadata.Description;
            this.constructorInfo = constructor;
        }

        public Value ExecuteOperation(Value[] args)
        {
            Operation operation = (Operation)constructorInfo.Invoke(null);
            operation.SetArguments(args);
            Value result = operation.Execute();
            if (result == null) {
                result = new NullValue();
            }
            return result;
        }
        

        // a valid operation is a class that inherits from Operation
        // and also has a default constructor with 0 arguments
        // and lastly the class must have the OperationAttribute
        public static List<OperationDefinition> GetValidOperationsInAssembly(Assembly asm) {
            List<OperationDefinition> operations = new List<OperationDefinition>();
            foreach (Type type in asm.GetTypes())
            {
                if (type.IsSubclassOf(typeof(Operation)))
                {
                    object[] attributes = type.GetCustomAttributes(typeof(OperationAttribute), true);
                    OperationAttribute? attribute = attributes.FirstOrDefault() as OperationAttribute;
                    if (attribute == null)
                    {
                        continue;
                    }
                    ConstructorInfo? constructor = type.GetConstructor(Type.EmptyTypes);
                    if (constructor == null)
                    {
                        continue;
                    }
                    operations.Add(new OperationDefinition(attribute, constructor));
                }
            }
            return operations;
        }

        public static OperationDefinition? GetOperationDefinitionFromType(Type t)
        {
            if (t.IsSubclassOf(typeof(Operation)))
            {
                object[] attributes = t.GetCustomAttributes(typeof(OperationAttribute), true);
                OperationAttribute? attribute = attributes.FirstOrDefault() as OperationAttribute;
                if (attribute == null)
                {
                    return null;
                }
                ConstructorInfo? constructor = t.GetConstructor(Type.EmptyTypes);
                if (constructor == null)
                {
                    return null;
                }
                return new OperationDefinition(attribute, constructor);
            }
            return null;
        }

        private static readonly Dictionary<string,OperationDefinition> operations = new Dictionary<string, OperationDefinition>();

        public static void LoadOperations(Assembly asm)
        {
            GetValidOperationsInAssembly(asm).ForEach(op => operations.Add(op.opName, op));
        }

        public static OperationDefinition? GetOperationDefinition(string name)
        {
            if (operations.ContainsKey(name))
            {
                return operations[name];
            }
            return null;
        }

        public static bool OperationExists(string name)
        {
            return operations.ContainsKey(name);
        }
    }
}
