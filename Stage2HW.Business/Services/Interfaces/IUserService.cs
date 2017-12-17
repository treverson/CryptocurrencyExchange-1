using Stage2HW.Business.Dtos;
using System.Collections.Generic;

namespace Stage2HW.Business.Services.Interfaces
{
    public interface IUserService
    {
        List<UserDto> GetExistingUsers();
        void AddUser(UserDto newUser);
        UserDto GetUser(string userLogin, string userPassword);
        double GetUserCryptocurrencyBalance(string currencyName);
    }
}