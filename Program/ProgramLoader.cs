using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using ByteCarft;

namespace ByteCraft.Program
{
    internal static class ProgramLoader
    {
        public static DirectoryInfo mainDir { get; private set;}
        public static CodeFile mainFile { get; private set;}
        public static ProgramSettings settings { get; private set;}
        // Load the program from a file
        // Path should be the path to the folder containing the file proj.json
        public static void LoadProgram(string path)
        {
            DirectoryInfo dirInfo = new DirectoryInfo(path);
            if (!dirInfo.Exists)
            {
                Console.WriteLine("Directory does not exist");
                return;
            }
            mainDir = dirInfo;
            FileInfo projFile = dirInfo.GetFiles("proj.json").FirstOrDefault();
            if (projFile == null)
            {
                Console.WriteLine("Program file \"proj.json\" not found");
                return;
            }
            string jsonString = File.ReadAllText(projFile.FullName);
            settings = ProgramSettings.FromJson(jsonString);
            mainFile = new CodeFile(new FileInfo(Path.Combine(mainDir.FullName,settings.entry)));
            
        }
    }
}
