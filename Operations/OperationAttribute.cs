using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ByteCraft.Operations
{
    [AttributeUsage(AttributeTargets.Class,Inherited = true, AllowMultiple = false)]
    public class OperationAttribute : Attribute
    {
        public string Name { get; private set; }
        public string Description { get; set; }
        public OperationAttribute(string name)
        {
            Name = name;
            Description = "No Description Available";
        }

        public OperationAttribute(string name, string description)
        {
            Name = name;
            Description = description;
        }

    }
}
