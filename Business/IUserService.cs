using System.Collections.Generic; 
using CliniCareApp.Models; 

namespace CliniCareApp.Business
{
    public interface IUserService
    {
        void CreateUser(string inputUserName, string inputPassword, string inputEmail, string inputAccessKey);
        bool Authenticate(string userName, string password);
        User? GetUserByUserName(string inputUserName);
    }
}
