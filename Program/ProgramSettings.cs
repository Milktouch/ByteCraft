using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ByteCraft.Program
{
    internal class ProgramSettings
    {
        public string name { get; set; }
        public string description { get; set; }
        public string author { get; set; }
        public string version { get; set; }
        public string entry { get; set; }
        public List<string> dependencies { get; set; }

        public static ProgramSettings FromJson(string jsonStr)
        {
            ProgramSettings settings = JsonSerializer.Deserialize<ProgramSettings>(jsonStr);
            return settings;
        }
    }
}
