using System.Collections.Generic; 
using CliniCareApp.Models; 

namespace CliniCareApp.Business
{
    public interface IUserService
    {
        User CreateUser(string userName, string password, string email, string accessKey);
        bool Authenticate(string userName, string password);
        List<User> GetAllUsers(); 
        User? GetUserByUserName(string userName);
        User? GetUserByEmail(string userEmail);
        public void UpdateUserDetails(string userEmail, UserUpdateDTO UserUpdate);
        public void DeleteUser(string userEmail);
    }
}
