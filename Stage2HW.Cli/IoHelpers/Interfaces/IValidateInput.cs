namespace Stage2HW.Cli.IoHelpers.Interfaces
{
    interface IValidateInput
    {
        string ValidateName();
        string ValidatePassword();
        string ValidateNickName();
        void PauseLoop();
    }
}