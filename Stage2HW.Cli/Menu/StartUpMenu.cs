using Stage2HW.Cli.IoHelpers.Interfaces;
using Stage2HW.Cli.Menu.Interfaces;
using Stage2HW.Cli.Menu.MenuOptions;
using System.Collections.Generic;
using System.Linq;

namespace Stage2HW.Cli.Menu
{
    internal class StartUpMenu : IMenu
    {
        private readonly List<MenuOption> _options = new List<MenuOption>();

        private readonly IRegisterToExchange _registerUser;
        private readonly IConsoleWriter _consoleWriter;
        private readonly IInputReader _inputReader;
        private readonly ILogInToExchange _logInToExchange;

        public StartUpMenu(IConsoleWriter consoleWriter, IInputReader inputReader, IRegisterToExchange registerUser, ILogInToExchange logInToExchange)
        {
            _consoleWriter = consoleWriter;
            _inputReader = inputReader;
            _registerUser = registerUser;
            _logInToExchange = logInToExchange;

            AddOptions();
        }

        public bool Exit { get; set; }

        public void AddOptions()
        {
            _options.Add(new MenuOption("Log in", _logInToExchange.LogInUserToExchange));
            _options.Add(new MenuOption("Register", _registerUser.RegisterUserToExchange));
            _options.Add(new MenuOption("Exit"));

            foreach (var option in _options)
            {
                option.OptionNumber = _options.IndexOf(option)+1;
            }
        }

        public void PrintMenu()
        {
            _consoleWriter.WriteMessage("################# STAGE2 HOMEWORK #################\n");
            _consoleWriter.WriteMessage("############# CRYPTOCURRENCY EXCHANGE #############\n");

            foreach (var option in _options)
            {
                _consoleWriter.WriteMessage($"{_options.IndexOf(option)+1}. {option.Name}\n");
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

                if (choice != 0 && choice <= _options.Count)
                {
                     menuOption = _options.SingleOrDefault(opt=> opt.OptionNumber == _options[choice-1].OptionNumber);
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
