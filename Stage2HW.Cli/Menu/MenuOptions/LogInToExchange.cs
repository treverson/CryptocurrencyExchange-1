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
        private readonly IMenu _loggedInMenu;

        public LogInToExchange(IUserService userService, IConsoleWriter consoleWriter, IInputReader inputReader, IValidateInput validateInput, IMenu loggedInMenu)
        {
            _userService = userService;
            _consoleWriter = consoleWriter;
            _inputReader = inputReader;
            _validateInput = validateInput;
            _loggedInMenu = loggedInMenu;
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

            RunLoggedInMenu(activeUser);
            _loggedInMenu.Exit = false;
        }

        public void RunLoggedInMenu(UserDto activeUser)
        {
            _loggedInMenu.ActiveUser = activeUser;
            while (!_loggedInMenu.Exit)
            {
                _loggedInMenu.PrintMenu();
                _loggedInMenu.RunOption();
                _consoleWriter.ClearConsole();
            }
        }
    }
}

