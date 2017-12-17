using Stage2HW.Cli.IoHelpers.Interfaces;
using Stage2HW.Cli.Menu.Interfaces;
using Stage2HW.Cli.Menu.MenuOptions;
using Stage2HW.Cli.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using Ninject;

namespace Stage2HW.Cli.Menu
{
    internal class MainMenu : IMenu
    {
        private readonly List<MenuOption> _options = new List<MenuOption>();

        private readonly IConsoleWriter _consoleWriter;
        private readonly IInputReader _inputReader;
        private readonly ICryptocurrencyExchange _cryptocurrencyExchange;
        private readonly IAccountOperations _accountOperations;
        private readonly IShowUser _showUser;

        public MainMenu(IConsoleWriter consoleWriter, IInputReader inputReader, ICryptocurrencyExchange cryptocurrencyExchange, IAccountOperations accountOperations, IShowUser showUser)
        {
            _consoleWriter = consoleWriter;
            _inputReader = inputReader;
            _cryptocurrencyExchange = cryptocurrencyExchange;
            _accountOperations = accountOperations;
            _showUser = showUser;

            AddOptions();
        }

        public bool Exit { get; set; }

        public void AddOptions()
        {
            _options.Add(new MenuOption("Check BitBay exchange", _cryptocurrencyExchange.RunExchange));
            _options.Add(new MenuOption("Deposit funds", _accountOperations.DepositFunds));
            _options.Add(new MenuOption("Withdraw funds", _accountOperations.WithdrawFunds));
            _options.Add(new MenuOption("Buy currencies", _accountOperations.BuyCurrencies));
            _options.Add(new MenuOption("Sell currencies",_accountOperations.SellCurrencies));
            _options.Add(new MenuOption("View account history", _accountOperations.ViewHistory));
            _options.Add(new MenuOption("Logout"));

            foreach (var option in _options)
            {
                option.OptionNumber = _options.IndexOf(option)+1;
            }
        }

        public void DisplayHeader()
        {
            _consoleWriter.ClearConsole();
            _consoleWriter.WriteMessage("############# CRYPTOCURRENCY EXCHANGE #############\n");
            _consoleWriter.WriteMessage($"# Logged in as: {_showUser.ActiveUser.Login}\n");
        }

        public void PrintMenu()
        {
            DisplayHeader();

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

                if (choice != 0 && choice <= _options.Count)
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
