using Stage2HW.Business.Dtos;
using Stage2HW.Business.Services.Interfaces;
using Stage2HW.Cli.IoHelpers.Interfaces;
using Stage2HW.Cli.Menu.Interfaces;

namespace Stage2HW.Cli.Menu.MenuOptions
{
    internal class LogInToExchange : ILogInToExchange
    {
        private readonly IConsoleWriter _consoleWriter;
        private readonly IInputReader _inputReader;
        private readonly IValidateInput _validateInput;
        private readonly IUserService _userService;
        private readonly IMenu _mainMenu;

        public LogInToExchange(IUserService userService, IConsoleWriter consoleWriter, IInputReader inputReader, IValidateInput validateInput, IMenu mainMenu)
        {
            _userService = userService;
            _consoleWriter = consoleWriter;
            _inputReader = inputReader;
            _validateInput = validateInput;
            _mainMenu = mainMenu;
        }

        public void LogInUserToExchange()
        {
            _consoleWriter.ClearConsole();
            _consoleWriter.WriteMessage("##### CRYPTOCURRENCY EXCHANGE #####\n");
            _consoleWriter.WriteMessage("# Log in \n");
            _consoleWriter.WriteMessage("  User name: ");
            string userNickName = _inputReader.ReadInput();
            _consoleWriter.WriteMessage("  Password: ");
            string userPassword = _inputReader.ReadInput();

            var activeUser = _userService.GetUser(userNickName, userPassword);

            if (activeUser == null)
            {
                _consoleWriter.WriteMessage("Wrong user name or password.");
                _validateInput.PauseLoop();
                return;
            }

            RunMainMenu(activeUser);
            _mainMenu.Exit = false;
        }

        public void RunMainMenu(UserDto activeUser)
        {
            _mainMenu.ActiveUser = activeUser;
            while (!_mainMenu.Exit)
            {
                _mainMenu.PrintMenu();
                _mainMenu.RunOption();
                _consoleWriter.ClearConsole();
            }
        }
    }
}

