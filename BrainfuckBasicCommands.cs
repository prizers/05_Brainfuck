using System;
using System.Collections.Generic;
using System.Linq;

namespace func.brainfuck
{
    public class BrainfuckBasicCommands
    {
        public static void RegisterTo(IVirtualMachine vm, Func<int> read, Action<char> write)
        {
            vm.RegisterCommand('.', b => write(Convert.ToChar(b.Memory[b.MemoryPointer])));
            vm.RegisterCommand('+', b =>
            {
                b.Memory[b.MemoryPointer] = Convert.ToByte((b.Memory[b.MemoryPointer] + 1) & 0xFF);
            });
            vm.RegisterCommand('-', b =>
            {
                b.Memory[b.MemoryPointer] = Convert.ToByte((b.Memory[b.MemoryPointer] + 255) & 0xFF);
            });
            vm.RegisterCommand(',', b => { b.Memory[b.MemoryPointer] = Convert.ToByte(read()); });
            vm.RegisterCommand('>', b => { b.MemoryPointer = (b.MemoryPointer + 1) % b.Memory.Length; });
            vm.RegisterCommand('<', b =>
            {
                b.MemoryPointer = (b.MemoryPointer + b.Memory.Length - 1) % b.Memory.Length;
            });
            const string literals = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            for (var ix = 0; ix < literals.Length; ix++)
            {
                var literal = literals[ix];
                vm.RegisterCommand(literal, b => { b.Memory[b.MemoryPointer] = Convert.ToByte(literal); });
            }
        }
    }
}
