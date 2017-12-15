using System.Collections.Generic;
using Stage2HW.DataAccess.Models;

namespace Stage2HW.DataAccess.Repositories.Interfaces
{
    public interface IUserRepository
    {
        List<User> GetExistingUsers();
        void AddUser(User newUser);
        User GetUser(string userLogin, string userPassword);
        List<Transaction> GetTransactionsHistory(int activeUserId);
        void RegisterTransaction(Transaction transaction);
        double UpdateUserTransactions(string currencyName);
    }
}