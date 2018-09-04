using System;
using System.Collections.Generic;

namespace func.brainfuck
{
    public class VirtualMachine : IVirtualMachine
    {
        public VirtualMachine(string program, int memorySize)
        {
            code = program;
            memory = new byte[memorySize];
            commands = new Dictionary<char, Action<IVirtualMachine>>();
        }

        public void RegisterCommand(char symbol, Action<IVirtualMachine> execute)
        {
            commands[symbol] = execute;
        }

        public string Instructions { get => code; }
        public int InstructionPointer { get; set; }
        public byte[] Memory { get => memory; }
        public int MemoryPointer { get; set; }
        public void Run()
        {
            MemoryPointer = 0;
            for (InstructionPointer = 0;
                InstructionPointer < Instructions.Length;
                InstructionPointer = InstructionPointer + 1)
            {
                char symbol = Instructions[InstructionPointer];
                if (commands.ContainsKey(symbol))
                    commands[symbol](this);
            }
        }

        private string code;
        private byte[] memory;
        private Dictionary<char, Action<IVirtualMachine>> commands;
    }
}
