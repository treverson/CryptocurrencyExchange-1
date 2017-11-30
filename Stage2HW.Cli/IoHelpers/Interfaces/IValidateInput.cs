﻿namespace Stage2HW.Cli.IoHelpers.Interfaces
{
    internal interface IValidateInput
    {
        string ValidateName();
        string ValidatePassword();
        string ValidateNickName();
        void PauseLoop();
    }
}