using System;
using Stage2HW.Cli.IoHelpers.Interfaces;

namespace Stage2HW.Cli.IoHelpers
{
    public class InputReader : IInputReader
    {
        public string ReadInput()
        {
            string input = Console.ReadLine();
            return input;
        }

        public void WaitForEnter()
        {
            ConsoleKeyInfo cki;
            do
            {
                cki = Console.ReadKey(true);
            }
            while (cki.Key != ConsoleKey.Enter);
        }

        public ConsoleKeyInfo ReadKey()
        {
            return Console.ReadKey(true);
        }
    }
}
