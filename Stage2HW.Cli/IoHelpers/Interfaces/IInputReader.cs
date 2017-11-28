using System;

namespace Stage2HW.Cli.IoHelpers.Interfaces
{
    public interface IInputReader
    {
        string ReadInput();
        ConsoleKeyInfo ReadKey();
        void WaitForEnter();
    }
}