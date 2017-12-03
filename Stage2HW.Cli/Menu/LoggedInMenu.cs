using Stage2HW.Business.Dtos;
using Stage2HW.Cli.IoHelpers.Interfaces;
using Stage2HW.Cli.Menu.Enums;
using Stage2HW.Cli.Menu.Interfaces;
using Stage2HW.Cli.Menu.MenuOptions;
using Stage2HW.Cli.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Stage2HW.Cli.Menu
{
    internal class LoggedInMenu : IMenu
    {
        private readonly List<MenuOption> _options = new List<MenuOption>();

        private readonly IConsoleWriter _consoleWriter;
        private readonly IInputReader _inputReader;
        private readonly ICryptocurrencyExchange _exchangeGenerator;

        public LoggedInMenu(IConsoleWriter consoleWriter, IInputReader inputReader, ICryptocurrencyExchange exchangeGenerator)
        {
            _consoleWriter = consoleWriter;
            _inputReader = inputReader;
            _exchangeGenerator = exchangeGenerator;

            AddOptions();
        }

        public UserDto ActiveUser { get; set; }
        public bool Exit { get; set; }

        public void AddOptions()
        {
            _options.Add(new MenuOption((int)LoggedInMenuEnum.CheckExchange, "Check exchange", _exchangeGenerator.RunExchange));
            _options.Add(new MenuOption((int)LoggedInMenuEnum.Logout, "Logout"));
        }

        public void PrintMenu()
        {
            _consoleWriter.ClearConsole();
            _consoleWriter.WriteMessage("##### CRYPTOCURRENCY EXCHANGE #####\n");
            _consoleWriter.WriteMessage($"# Logged in as: {ActiveUser.UserNickName}\n");

            for (int i = 0; i < _options.Count; i++)
            {
                _consoleWriter.WriteMessage($"{i + 1}. {_options[i].Name}\n");
            }
            _consoleWriter.WriteMessage("Choose option: ");
        }

        public void RunOption()
        {
            MenuOption menuOption = null;
            int choice;

            do
            {
                var userInput = _inputReader.ReadKey();
                int.TryParse(userInput.KeyChar.ToString(), out choice);

                if (choice != 0)
                {
                    menuOption = _options.SingleOrDefault(opt => opt.OptionNumber == choice);
                }
                if (menuOption == null)
                {
                    _consoleWriter.WriteMessage("\nInvalid option, choose again.");
                }

            }while (menuOption == null);

            if (choice == (int)LoggedInMenuEnum.Logout)
            {
                Exit = true;
                return;
            }
            else
            {
                menuOption.CallOption();
            }
        }
    }
}
