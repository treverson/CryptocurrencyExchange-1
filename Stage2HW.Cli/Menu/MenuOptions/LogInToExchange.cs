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
        private readonly IShowUser _showUser;

        public LogInToExchange(IUserService userService, IConsoleWriter consoleWriter, IInputReader inputReader, IValidateInput validateInput, IMenu mainMenu, IShowUser showUser)
        {
            _userService = userService;
            _consoleWriter = consoleWriter;
            _inputReader = inputReader;
            _validateInput = validateInput;
            _mainMenu = mainMenu;
            _showUser = showUser;
        }

        public void LogInUserToExchange()
        {
            _consoleWriter.ClearConsole();
            _consoleWriter.WriteMessage("############# CRYPTOCURRENCY EXCHANGE #############\n");
            _consoleWriter.WriteMessage("# Log in \n");
            _consoleWriter.WriteMessage("  Login: ");
            string userLogin = _inputReader.ReadInput();
            _consoleWriter.WriteMessage("  Password: ");
            string userPassword = _inputReader.ReadInput();

            _showUser.ActiveUser = _userService.GetUser(userLogin, userPassword);

            if (_showUser.ActiveUser == null)
            {
                _consoleWriter.WriteMessage("Wrong login or password.");
                _validateInput.PauseLoop();
                return;
            }

            RunMainMenu(_showUser.ActiveUser);
            _mainMenu.Exit = false;
        }

        public void RunMainMenu(UserDto activeUser)
        {
            while (!_mainMenu.Exit)
            {
                _mainMenu.PrintMenu();
                _mainMenu.RunOption();
                _consoleWriter.ClearConsole();
            }
        }
    }
}

