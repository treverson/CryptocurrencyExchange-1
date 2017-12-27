using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stage2HW.DataAccess.Models;
using Stage2HW.DataAccess.Repositories.Interfaces;

namespace Stage2HW.DataAccess.Repositories
{
    public class CloudUserRepository /*BaseAzureTableStorageRepository,*/ //IUserRepository 
    {
        //public CloudUserRepository() : base("UserTransactions")
        //{
            
        //}

        //public IEnumerable<User> GetExistingUsers()
        //{
        //   return TableReference.CreateQuery<User>().AsEnumerable();
        //}

        public void AddUser(User newUser)
        {
            throw new NotImplementedException();
        }

        public User GetUser(string userLogin, string userPassword)
        {
            throw new NotImplementedException();
        }

        public double GetUserCryptocurrencyBalance(string currencyName)
        {
            throw new NotImplementedException();
        }
    }
}
