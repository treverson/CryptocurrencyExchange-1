using Stage2HW.Cli.IoHelpers.Interfaces;
using Stage2HW.Cli.Menu.Interfaces;

namespace Stage2HW.Cli
{
    internal class ProgramLoop
    {
        private readonly IMenu _menu;
        private readonly IConsoleWriter _consoleWriter;

        public ProgramLoop(IMenu mainMenu, IConsoleWriter consoleWriter)
        {
            _menu = mainMenu;
            _consoleWriter = consoleWriter;
        }

        public void Run()
        {
            while (!_menu.Exit)
            {
                _menu.PrintMenu();
                _menu.RunOption();
                _consoleWriter.ClearConsole();
            }
        }
    }
}
