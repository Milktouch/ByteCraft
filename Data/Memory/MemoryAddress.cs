using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ByteCraft.Data.Memory
{
    public class MemoryAddress
    {

        private static Regex _addressRegex = new Regex(@"(\d+):(\d+)#(\d+)");
        internal int chunkIndex;
        internal int offset;
        internal int size;
        internal int referenceCount = 1;

        public string ToString()
        {
            return chunkIndex + ":" + offset + "#" + size;
        }
        internal MemoryAddress(int chunkIndex, int offset,int size)
        {
            this.chunkIndex = chunkIndex;
            this.offset = offset;
            this.size = size;
        }

        internal MemoryAddress(string address)
        {
            if(_addressRegex.IsMatch(address) == false)
                throw new ArgumentException("Invalid address format");
            string[] parts = _addressRegex.Match(address).Groups.Cast<Group>().Skip(1).Select(g => g.Value).ToArray();
            if (parts.Length != 3)
                throw new ArgumentException("Invalid address format");
            chunkIndex = int.Parse(parts[0]);
            offset = int.Parse(parts[1]);
            size = int.Parse(parts[2]);
        }
        internal byte[] GetBytes()
        {
            return new byte[size];
        }
    }
}
