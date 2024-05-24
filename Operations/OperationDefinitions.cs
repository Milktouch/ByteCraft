using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ByteCraft.Operations
{
    public class OperationDefinition
    {
        public string opName { get; }
        public Dictionary<string,string> nameTypeMap {
            get
            {
                Dictionary<string, string> copy = new Dictionary<string, string>();
                foreach (KeyValuePair<string, string> entry in nameTypeMap)
                {
                    copy.Add(entry.Key, entry.Value);
                }
                return copy;
            } 
            private set
            {
                nameTypeMap = value;
            }
        }
        
        public OperationDefinition(string name, Dictionary<string, string> parameters)
        {
            this.opName = name;
            this.nameTypeMap = parameters;
        }

        


    }
}
