using System.Collections.Generic;

namespace func.brainfuck
{
    public class BrainfuckLoopCommands
    {
        private class JumpMachine
        {
            private Dictionary<int, int> jumpers;

            public JumpMachine(string program)
            {
                jumpers = new Dictionary<int, int>();
                var openers = new Stack<int>();
                char[] brackets = { '[', ']' };
                var ix = -1;
                for (; ; )
                {
                    ix = program.IndexOfAny(brackets, ix + 1);
                    if (ix < 0) return;
                    switch (program[ix])
                    {
                        case '[':
                            openers.Push(ix);
                            break;
                        case ']':
                            var opener = openers.Pop();
                            jumpers[opener] = ix;
                            jumpers[ix] = opener;
                            break;
                    }
                }
            }

            public void OnBrace(IVirtualMachine vm)
            {
                var leftBrace = '[' == vm.Instructions[vm.InstructionPointer];
                var zeroValue = 0 == vm.Memory[vm.MemoryPointer];
                if (leftBrace != zeroValue) return;
                vm.InstructionPointer = jumpers[vm.InstructionPointer];
            }
        }

        public static void RegisterTo(IVirtualMachine vm)
        {
            var jumpMachine = new JumpMachine(vm.Instructions);
            vm.RegisterCommand('[', b => jumpMachine.OnBrace(b));
            vm.RegisterCommand(']', b => jumpMachine.OnBrace(b));
        }
    }
}

