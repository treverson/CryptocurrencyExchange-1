using System.Collections.Generic;
using System.Linq;
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

        public User LogInUser(string userNickName)
        {
            User loggedInUser;
            using (var dbContext = new CryptocurrencyExchangeDbContext())
            {
                loggedInUser = dbContext.UsersDbSet.SingleOrDefault(user =>
                    user.UserNickName == userNickName);
            }
            return loggedInUser;
        }
    }
}
