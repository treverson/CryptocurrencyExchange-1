using Stage2HW.Business.Dtos;
using Stage2HW.Business.Services.Interfaces;
using Stage2HW.Cli.IoHelpers.Interfaces;
using Stage2HW.Cli.Menu.Interfaces;

namespace Stage2HW.Cli.Menu.MenuOptions
{
    internal class RegisterToExchange : IRegisterToExchange
    {
        private readonly IConsoleWriter _consoleWriter;
        private readonly IValidateInput _validateInput;
        private readonly IUserService _userService;

        public RegisterToExchange(IConsoleWriter consoleWriter, IValidateInput validateInput, IUserService userService)
        {
            _consoleWriter = consoleWriter;
            _validateInput = validateInput;
            _userService = userService;
        }

        public void RegisterUserToExchange()
        {
            _consoleWriter.ClearConsole();
            _consoleWriter.WriteMessage("##### CRYPTOCURRENCY EXCHANGE #####\n");
            _consoleWriter.WriteMessage("# Register new user \n");

            _consoleWriter.WriteMessage("State your name: ");
            string userName = _validateInput.ValidateName();

            _consoleWriter.WriteMessage("Set your login: ");
            string userLogin = _validateInput.ValidateNickName();

            _consoleWriter.WriteMessage("Set your password: ");
            string userPassword = _validateInput.ValidatePassword();

            UserDto newUser = new UserDto
            {
                Name = userName,
                Login = userLogin,
                Password = userPassword
            };

            _userService.AddUser(newUser);
            _consoleWriter.WriteMessage($"User {newUser.Login} successfully registered!");
            _validateInput.PauseLoop();
        }
    }
}