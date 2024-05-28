using ByteCarft;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ByteCraft.Parsing
{
    enum LineType
    {
        VariableAssignment,
        Operation,
        SpecialAction,
        Section
    }
    internal class CodeLine
    {
        public CodeFile file { get; internal set; }
        public int lineNumber { get; internal set; }
        public string lineDescription { get; internal set; }
        public LineType lineType { get; internal set;}
        
    }
}
