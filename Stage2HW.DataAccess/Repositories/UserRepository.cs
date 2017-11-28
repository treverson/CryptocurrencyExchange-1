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
            List<User> users = null;

            using (var dbContext = new CryptocurrencyExchangeDbContext())
            {
                return users = dbContext.UsersDbSet.ToList();
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
    }
}
