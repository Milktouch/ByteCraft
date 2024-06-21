using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ByteCraft.Exceptions;

namespace ByteCraft.Operations
{
    [AttributeUsage(AttributeTargets.Class,Inherited = true, AllowMultiple = false)]
    public class OperationAttribute : Attribute
    {
        public string Name { get; private set; }
        public string Description { get; set; }
        public OperationAttribute(string name)
        {
            if (!ValidName(name))
            {
                throw new SyntaxError("Invalid Operation Name");
            }
            Name = name;
            Description = "No Description Available";
        }

        public OperationAttribute(string name, string description)
        {
            if (!ValidName(name))
            {
                throw new SyntaxError("Invalid Operation Name");
            }
            Name = name;
            Description = description;
        }
        
        private static bool ValidName(string name)
        {
            return Parsing.Parser.OperationWithoutArgumentsRegex.IsMatch(name);
        }

    }
}
