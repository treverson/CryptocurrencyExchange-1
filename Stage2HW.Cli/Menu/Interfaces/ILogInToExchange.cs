﻿using Stage2HW.Business.Dtos;

namespace Stage2HW.Cli.Menu.Interfaces
{
    internal interface ILogInToExchange
    {
        void LogInUserToExchange();
        void RunLoggedInMenu(UserDto activeUser);
    }
}