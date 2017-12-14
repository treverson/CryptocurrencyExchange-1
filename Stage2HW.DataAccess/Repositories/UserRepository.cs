using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Stage2HW.DataAccess.Data;
using Stage2HW.DataAccess.Models;
using Stage2HW.DataAccess.Repositories.Interfaces;

namespace Stage2HW.DataAccess.Repositories
{
    public class UserRepository : IUserRepository
    {
        public List<User> GetExistingUsers()
        {
            using (var dbContext = new CryptocurrencyExchangeDbContext())
            {
                return dbContext.UsersDbSet.ToList();
            }
        }

        public void AddUser(User newUser)
        {
            using (var dbContext = new CryptocurrencyExchangeDbContext())
            {
                dbContext.UsersDbSet.Add(newUser);
                dbContext.SaveChanges();
            }
        }

        public User GetUser(string userLogin, string userPassword)
        {
            User loggedInUser;
            using (var dbContext = new CryptocurrencyExchangeDbContext())
            {
                loggedInUser = dbContext.UsersDbSet.SingleOrDefault(user => user.Login == userLogin);
                
                if (loggedInUser != null && loggedInUser.Password != userPassword)
                {
                    return null;
                }
            }
            return loggedInUser;
        }

        //public void RegisterDeposit(Transaction deposit)
        //{
        //    using (var dbContext = new CryptocurrencyExchangeDbContext())
        //    {
        //        dbContext.TransactionsDbSet.Add(deposit);
        //        dbContext.SaveChanges();
        //    }
        //}

        public List<Transaction> GetTransactionsHistory(int activeUserId)
        {
           //new List<Transaction>();

            using (var dbContext = new CryptocurrencyExchangeDbContext())
            {
                /* List<Transaction> transactionsHistory =*/
                return dbContext.TransactionsDbSet.Where(t => t.UserId == activeUserId).ToList();
            }
        }

        //public void RegisterWithdrawal(Transaction withdrawal)
        //{
        //    using (var dbContext = new CryptocurrencyExchangeDbContext())
        //    {
        //        dbContext.TransactionsDbSet.Add(withdrawal);
        //        dbContext.SaveChanges();
        //    }
        //}

        public void RegisterTransaction(Transaction transaction)
        {
            using (var dbContext = new CryptocurrencyExchangeDbContext())
            {
                dbContext.TransactionsDbSet.Add(transaction);
                dbContext.SaveChanges();
            }
        }

        public void RegisterBuyingCurrency(Transaction transaction)
        {
            using (var dbContext = new CryptocurrencyExchangeDbContext())
            {
                dbContext.TransactionsDbSet.Add(transaction);
                dbContext.SaveChanges();
            }
        }
    }
}
