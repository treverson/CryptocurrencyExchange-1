using Stage2HW.Business.Dtos;
using Stage2HW.Cli.IoHelpers.Interfaces;
using Stage2HW.Cli.Menu.Interfaces;
using Stage2HW.Cli.Menu.MenuOptions;
using Stage2HW.Cli.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Stage2HW.Cli.Menu
{
    internal class MainMenu : IMenu
    {
        private readonly List<MenuOption> _options = new List<MenuOption>();

        private readonly IConsoleWriter _consoleWriter;
        private readonly IInputReader _inputReader;
        private readonly ICryptocurrencyExchange _cryptoccurrencyExchange;


        public MainMenu(IConsoleWriter consoleWriter, IInputReader inputReader, ICryptocurrencyExchange crypotcurrencyExchange)
        {
            _consoleWriter = consoleWriter;
            _inputReader = inputReader;
            _cryptoccurrencyExchange = crypotcurrencyExchange;

            AddOptions();
        }

        public UserDto ActiveUser { get; set; }
        public bool Exit { get; set; }

        public void AddOptions()
        {
            _options.Add(new MenuOption("Check exchange generated", _cryptoccurrencyExchange.RunExchange));
            _options.Add(new MenuOption("Logout"));

            foreach (var option in _options)
            {
                option.OptionNumber = _options.IndexOf(option)+1;
            }
        }

        public void PrintMenu()
        {
            _consoleWriter.ClearConsole();
            _consoleWriter.WriteMessage("##### CRYPTOCURRENCY EXCHANGE #####\n");
            _consoleWriter.WriteMessage($"# Logged in as: {ActiveUser.UserNickName}\n");

            foreach (var option in _options)
            {
                _consoleWriter.WriteMessage($"{_options.IndexOf(option) + 1}. {option.Name}\n");
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
                    menuOption = _options.SingleOrDefault(opt => opt.OptionNumber == _options[choice - 1].OptionNumber);
                }
                if (menuOption == null)
                {
                    _consoleWriter.WriteMessage("\nInvalid option, choose again.");
                }

            } while (menuOption == null);

            if (choice == _options.Last().OptionNumber)
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
