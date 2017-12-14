using System;
using Stage2HW.Cli.IoHelpers.Interfaces;

namespace Stage2HW.Cli.IoHelpers
{
    internal class ConsoleWriter : IConsoleWriter
    {
        public void WriteMessage(string message)
        {
            Console.Write(message);
        }

        public void WriteMessageInColor(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(message);
            Console.ResetColor();
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
