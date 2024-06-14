﻿using ByteCraft.Data;
using ByteCraft.Operations;

namespace ByteCraft.BasicOperations
{
    [Operation("input", "this operation reads a string from the standard console input")]
    internal class Input : Operation
    {
        public Input() { }
        public override Value Execute()
        {
            string? input = Console.ReadLine();
            input = input == null ? "" : input;
            return new StringValue(input);
        }
    }
}
