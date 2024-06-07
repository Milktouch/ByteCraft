using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ByteCraft.Data.Memory
{
    internal class MemoryChunk
    {
        private static List<MemoryChunk> _chunks = new List<MemoryChunk>();
        public static readonly int DefaultSize = 128;
        internal static bool ValidateAddressSize(int size)
        {
            return size > 0;
        }
        private int _chunkIndex;
        private byte[] _data;
        internal int _freeMem { get; private set; }
        private Dictionary<int, MemoryAddress> _allocations;
        public MemoryChunk(int size)
        {
            _chunkIndex = _chunks.Count;
            _data = new byte[size];
            _allocations = new Dictionary<int, MemoryAddress>(size);
            _chunks.Add(this);
            _freeMem = size;
        }
        public MemoryChunk()
        {
            _chunkIndex = _chunks.Count;
            _data = new byte[DefaultSize];
            _allocations = new Dictionary<int, MemoryAddress>(DefaultSize);
            _chunks.Add(this);
            _freeMem = DefaultSize;
        }
        public MemoryAddress Allocate(int size)
        {
            if (size < 1)
                throw new ArgumentException("Invalid Memory size");
            if (_freeMem < size)
                throw new OutOfMemoryException("Not enough memory");
            int offset = 0;
            int allocated = 0;
            while (allocated < size && allocated + offset < _data.Length)
            {
                MemoryAddress address = _allocations.GetValueOrDefault(offset + allocated, null);
                if (address == null)
                {
                    allocated++;
                }
                else
                {
                    offset = address.offset + address.size;
                    allocated = 0;
                }
            }
            if (allocated < size)
            {
                throw new OutOfMemoryException("Not enough memory");
            }
            MemoryAddress memoryAddress = new MemoryAddress(_chunkIndex, offset, size);
            _allocations[offset] = memoryAddress;
            _freeMem -= size;
            return memoryAddress;
        }
        public byte[] Read(MemoryAddress address)
        {
            if (address.size + address.offset > _data.Length)
            {
                throw new IndexOutOfRangeException("Invalid Address. Address out of range");
            }
            byte[] data = new byte[address.size];
            for (int i = 0; i < address.size; i++)
            {
                data[i] = _data[address.offset + i];
            }
            return data;
        }
        public void MoveMemory()
        {
            int gapsClosed = 0;
            int index = 0;
            int start = 0;
            while (index < _data.Length)
            {
                if (gapsClosed==_freeMem)
                {
                    return;
                }
                MemoryAddress address = _allocations.GetValueOrDefault(index, null);
                while (address == null)
                {
                    index++;
                    address = _allocations.GetValueOrDefault(index, null);
                }
                if (index == start)
                {
                    index += address.size;
                    continue;
                }
                gapsClosed+= index - start;
                for (int i = 0; i < address.size; i++)
                {
                    _data[start + i] = _data[index + i];
                }
                _allocations.Remove(address.offset);
                _allocations[start] = address;
                address.offset = start;
                index = start + address.size;
                start = index;
            }
        }
        public void Free(MemoryAddress address)
        {
            if (_allocations.ContainsKey(address.offset))
            {
            }
            _allocations.Remove(address.offset);
            _freeMem += address.size;
        }
        public void PrintChunk()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("------Chunk: " + _chunkIndex);
            int index = 0;
            while (index < _data.Length)
            {
                Console.ResetColor();
                MemoryAddress address = _allocations.GetValueOrDefault(index, null);
                if (address == null)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write(_data[index] + ",");
                    index++;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    for (int i = 0; i < address.size; i++)
                    {
                        Console.Write(_data[index] + ",");
                        index++;
                    }
                }
            }
            Console.ResetColor();
            Console.WriteLine();
        }
        public void Write(MemoryAddress address, byte[] data)
        {
            if (address.size != data.Length)
                throw new ArgumentException("Invalid data size");
            if (address.size + address.offset > _data.Length)
            {
                throw new IndexOutOfRangeException("Invalid Address. Address out of range");
            }
            for (int i = 0; i < address.size; i++)
            {
                _data[address.offset + i] = data[i];
            }
        }   

        public static MemoryChunk GetChunk(int index)
        {

            return _chunks[index];
        }
    }
}
