using System.Collections.Generic;
using Stage2HW.Business.Dtos;

namespace Stage2HW.Business.Services.Interfaces
{
    public interface IUserService
    {
        List<UserDto> GetExistingUsers();
        void AddUser(UserDto newUser);
        UserDto GetUser(string userLogin, string userPassword);
        //void RegisterDeposit(string userLogin, double userDeposit);
      //  void RegisterDeposit(TransactionDto deposit);
        List<TransactionDto> GetTransactionHistory(int activeUserId);
       // void RegisterWithdrawal(TransactionDto withdrawal);
        void RegisterTransaction(TransactionDto transaction);
    }
}