using Stage2HW.Business.Dtos;
using System.Collections.Generic;

namespace Stage2HW.Business.Services.Interfaces
{
    public interface IUserService
    {
        List<UserDto> GetExistingUsers();
        void AddUser(UserDto newUser);
        UserDto GetUser(string userLogin, string userPassword);
        List<TransactionDto> GetTransactionHistory(int activeUserId);
        void RegisterTransaction(TransactionDto transaction);
        double GetUserCryptocurrencyBalance(string currencyName);
    }
}