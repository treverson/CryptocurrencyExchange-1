using System;
using System.Linq;
using Stage2HW.Business.Services.Interfaces;
using Stage2HW.Cli.IoHelpers.Interfaces;

namespace Stage2HW.Cli.IoHelpers
{
    class ValidateInput : IValidateInput
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

            while (String.IsNullOrEmpty(userInput) || !userInput.All(char.IsLetter))
            {
                _consoleWriter.WriteMessage("Name cannot be empty, contain special characters or numbers.\nTry again.");
                userInput = _inputReader.ReadInput();
            }
      
            return userInput;
        }

        public string ValidatePassword()
        {
            string userInput = _inputReader.ReadInput();

            while (String.IsNullOrWhiteSpace(userInput) || userInput.Length > 10)
            {
                _consoleWriter.WriteMessage("Password cannot be empty or be longer than 10 characters.\nTry again.");
                userInput = _inputReader.ReadInput();
            }

            return userInput;
        }

        public string ValidateNickName()
        {
            string userInput = _inputReader.ReadInput();

            while (String.IsNullOrWhiteSpace(userInput))
            {
                _consoleWriter.WriteMessage("Nickname cannot be empty.\nTry again.");
                userInput = _inputReader.ReadInput();
            }
            if (_userService.GetExistingUsers().Any(user => user.UserNickName == userInput))
            {
                _consoleWriter.WriteMessage("Nickname already in use.\nTry another.");
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
