using System;

namespace Stage2HW.Cli.IoHelpers.Interfaces
{
    internal interface IInputReader
    {
        string ReadInput();
        ConsoleKeyInfo ReadKey();
        void WaitForEnter();
    }
}