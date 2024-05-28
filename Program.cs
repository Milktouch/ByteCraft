

using ByteCraft.Data.Memory;

MemoryChunk chunk = new MemoryChunk();

MemoryAddress address = chunk.Allocate(10);
chunk.PrintChunk();
MemoryAddress address2 = chunk.Allocate(10);
chunk.PrintChunk();
chunk.Free(address);
chunk.PrintChunk();
address = chunk.Allocate(15);
chunk.PrintChunk();
chunk.MoveMemory();
chunk.PrintChunk();




