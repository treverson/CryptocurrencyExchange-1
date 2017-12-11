using Stage2HW.Business.Dtos;
using Stage2HW.Business.Services.Interfaces;
using Stage2HW.Cli.IoHelpers.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Stage2HW.Cli.IoHelpers
{
    internal class ValidateInput : IValidateInput
    {
        private readonly IInputReader _inputReader;
        private readonly IConsoleWriter _consoleWriter;
        private readonly IUserService _userService;

        public ValidateInput(IInputReader inputReader, IConsoleWriter consoleWriter, IUserService userService)
        {
            _inputReader = inputReader;
            _consoleWriter = consoleWriter;
            _userService = userService;
        }

        public string ValidateName()
        {
            string userInput = _inputReader.ReadInput();

            while (string.IsNullOrEmpty(userInput) || !userInput.All(char.IsLetter))
            {
                _consoleWriter.WriteMessage("Name cannot be empty, contain special characters or numbers.\nTry again: ");
                userInput = _inputReader.ReadInput();
            }

            return userInput;
        }

        public string ValidatePassword()
        {
            string userInput = _inputReader.ReadInput();

            while (string.IsNullOrWhiteSpace(userInput) || userInput.Length > 10)
            {
                _consoleWriter.WriteMessage("Password cannot be empty or be longer than 10 characters.\nTry again: ");
                userInput = _inputReader.ReadInput();
            }

            return userInput;
        }

        public string ValidateNickName()
        {
            string userInput = _inputReader.ReadInput();
            List<UserDto> existingUsers = _userService.GetExistingUsers();

            while (existingUsers.Any(user => user.UserNickName == userInput.ToLower()) || string.IsNullOrWhiteSpace(userInput))
            {
                _consoleWriter.WriteMessage("Nickname already in use or invalid characters.\nTry again: ");
                userInput = _inputReader.ReadInput();
            }

            return userInput;
        }

        public void PauseLoop()
        {
            _consoleWriter.WriteMessage("\nPress ENTER to continue");
            _inputReader.WaitForEnter();
        }
    }
}
