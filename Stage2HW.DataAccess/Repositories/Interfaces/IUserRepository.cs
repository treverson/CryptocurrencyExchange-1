using System.Collections.Generic;
using Stage2HW.DataAccess.Models;

namespace Stage2HW.DataAccess.Repositories.Interfaces
{
    public interface IUserRepository
    {
        List<User> GetExistingUsers();
        void AddUser(User newUser);
        User GetUser(string userLogin, string userPassword);
        bool CheckUserPassword(User userDto);//new
        User GetUserByLogin(string login);
    }
}