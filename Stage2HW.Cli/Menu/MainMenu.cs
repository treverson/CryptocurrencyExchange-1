using Stage2HW.Cli.IoHelpers.Interfaces;
using Stage2HW.Cli.Menu.Enums;
using Stage2HW.Cli.Menu.Interfaces;
using Stage2HW.Cli.Menu.MenuOptions;
using System;
using System.Collections.Generic;
using System.Linq;
using Stage2HW.Business.Dtos;
using Stage2HW.Cli.Menu.MenuOptions.Interfaces;

namespace Stage2HW.Cli.Menu
{
    class MainMenu : IMenu
    {
        private readonly List<MenuOption> _options = new List<MenuOption>();

        private readonly IRegisterToExchange _registerUser;
        private readonly IConsoleWriter _consoleWriter;
        private readonly IInputReader _inputReader;
        private readonly ILogInToExchange _logInToExchange;

        public MainMenu(IConsoleWriter consoleWriter, IInputReader inputReader, IRegisterToExchange registerUser, ILogInToExchange logInToExchange)
        {
            _consoleWriter = consoleWriter;
            _inputReader = inputReader;
            _registerUser = registerUser;
            _logInToExchange = logInToExchange;

            AddOptions();
        }

        public bool Exit { get; set; }
        public UserDto ActiveUser { get; set; }

        public void AddOptions()
        {
            _options.Add(new MenuOption((int)MainMenuEnum.LogIn, "Log in", _logInToExchange.LogInUserToExchange));
            _options.Add(new MenuOption((int)MainMenuEnum.Register, "Register", _registerUser.RegisterUserToExchange));
            _options.Add(new MenuOption((int)MainMenuEnum.Exit, "Exit"));
        }

        public void PrintMenu()
        {
            _consoleWriter.WriteMessage("######## STAGE2 HOMEWORK 1 ########\n");
            _consoleWriter.WriteMessage("##### CRYPTOCURRENCY EXCHANGE #####\n");

            for (int i = 0; i < _options.Count; i++)
            {
                _consoleWriter.WriteMessage($"{i + 1}. {_options[i].Name}\n");
            }
            _consoleWriter.WriteMessage("Choose option: ");
        }

        public void RunOption()
        {
            var userInput = _inputReader.ReadKey();
             
            while (userInput.Key != ConsoleKey.D1 && userInput.Key != ConsoleKey.D2 && userInput.Key != ConsoleKey.D3)
            {
                _consoleWriter.WriteMessage("\nInvalid option, choose again: ");
                userInput = _inputReader.ReadKey();
                
            }

            int choice = Convert.ToInt32(userInput.KeyChar.ToString());

            var menuOption = _options.SingleOrDefault(opt => opt.OptionNumber == choice);

            if (choice == (int)MainMenuEnum.Exit)
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
