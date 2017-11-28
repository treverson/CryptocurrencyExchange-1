using Stage2HW.Business.Dtos;
using Stage2HW.Business.Services.Interfaces;
using Stage2HW.Cli.IoHelpers.Interfaces;
using Stage2HW.Cli.Menu.Interfaces;

namespace Stage2HW.Cli.Menu.MenuOptions
{
    internal class RegisterToExchange : IRegisterToExchange
    {
        private readonly IConsoleWriter _consoleWriter;
        private readonly IInputReader _inputReader;
        private readonly IValidateInput _validateInput;
        private readonly IUserService _userService;

        public RegisterToExchange(IConsoleWriter consoleWriter, IInputReader inputReader, IValidateInput validateInput, IUserService userService)
        {
            _consoleWriter = consoleWriter;
            _inputReader = inputReader;
            _validateInput = validateInput;
            _userService = userService;
        }

        public void RegisterUserToExchange()
        {
            _consoleWriter.ClearConsole();
            _consoleWriter.WriteMessage("# Register new user in Cryptocurrency Exchange #\n");
            _consoleWriter.WriteMessage("State your name: ");
            string userName = _validateInput.ValidateName();
            _consoleWriter.WriteMessage("Set your nickname: ");
            string userNickName = _validateInput.ValidateNickName();
            _consoleWriter.WriteMessage("Set your password: ");
            string userPassword = _validateInput.ValidatePassword();
       
            UserDto newUser = new UserDto()
            {
                UserName = userName,
                UserNickName = userNickName,
                UserPassword = userPassword
            };

            _userService.AddUser(newUser);
            _consoleWriter.WriteMessage($"User {newUser.UserNickName} successfuly registered!");
            _validateInput.PauseLoop();
        }
    }
}

// old version;

//public void PrintRegistrationForm()
//{
//    _consoleWriter.ClearConsole();
//    _consoleWriter.WriteMessage("# Register new user in Cryptocurrency Exchange #");
//    _consoleWriter.WriteMessage("\nState your name: ");
//    _consoleWriter.WriteMessage("\nSet your nickname: ");
//    _consoleWriter.WriteMessage("\nSet your password: ");
//}

//public void RegisterUserToExchange()
//{
//_consoleWriter.ClearConsole();
//_consoleWriter.WriteMessage("# Register new user in Cryptocurrency Exchange #");
//_consoleWriter.WriteMessage("\nState your name: ");
//_consoleWriter.WriteMessage("\nSet your nickname: ");
//_consoleWriter.WriteMessage("\nSet your password: ");
//_consoleWriter.SetCursorPosition(17, 1);
//string userName = _validateInput.ValidateName();
//_consoleWriter.SetCursorPosition(19, 2);
//string userNickName = _validateInput.ValidateNickName();
//_consoleWriter.SetCursorPosition(19, 3);
//string userPassword = _validateInput.ValidatePassword();

//UserDto newUser = new UserDto()
//{
//    UserName = userName,
//    UserNickName = userNickName,
//    UserPassword = userPassword
//};

//_userService.AddUser(newUser);
//_consoleWriter.WriteMessage("You have successfuly registered!");

//}