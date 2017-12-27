using System;
using Stage2HW.DataAccess.Data;
using Stage2HW.DataAccess.Models;
using Stage2HW.DataAccess.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;

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

        public double GetUserCryptocurrencyBalance(string currencyName)
        {
            using (var dbContext = new CryptocurrencyExchangeDbContext())
            {
                return Math.Round(dbContext.TransactionsDbSet.Where(c => c.CurrencyName == currencyName).Sum(x => x.Amount), 7);
            }
        }
    }
}
