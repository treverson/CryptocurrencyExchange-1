using System;
using Stage2HW.Cli.IoHelpers.Interfaces;

namespace Stage2HW.Cli.IoHelpers
{
    class ConsoleWriter : IConsoleWriter
    {
        public void WriteMessage(string message)
        {
            Console.Write(message);
        }

        public void ClearConsole()
        {
            Console.Clear();
        }

        public void SetCursorPosition(int left, int top)
        {
            Console.SetCursorPosition(left, top);
        }
    }
}
