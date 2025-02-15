﻿using System;
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
        internal readonly ConstructorInfo constructorInfo;
        internal OperationDefinition(OperationAttribute metadata,ConstructorInfo constructor)
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
        private static List<OperationDefinition> GetValidOperationsInAssembly(Assembly asm) {
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

        internal static OperationDefinition? GetOperationDefinitionFromType(Type t)
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

        private static readonly Dictionary<Assembly,OperationDefinition[]> loadedOperations = new Dictionary<Assembly, OperationDefinition[]>();

        internal static List<OperationDefinition> LoadOperations(Assembly asm)
        {
            if (loadedOperations.ContainsKey(asm))
            {
                return loadedOperations[asm].ToList();
            }
            var operations = GetValidOperationsInAssembly(asm);
            OperationDefinition.loadedOperations[asm] = operations.ToArray();
            return operations;
        }

        

        
    }
}
