﻿using System.Collections.Generic;
using Stage2HW.Business.Dtos;

namespace Stage2HW.Business.Services.Interfaces
{
    public interface IUserService
    {
        List<UserDto> GetExistingUsers();
        void AddUser(UserDto newUser);
        UserDto LogInUser(string userName, string userPassword);
    }
}