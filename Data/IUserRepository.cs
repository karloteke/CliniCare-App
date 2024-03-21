using CliniCareApp.Models;

namespace CliniCareApp.Data
{
    public interface IUserRepository
    {
        void AddUser(User user);
        User? GetUserByUserName(string userName);
        List<User> GetUsers();
        User? GetUserByEmail(string Email);
        User? GetUserById (int? UserId);
        void UpdateUser(User user);
        void DeleteUser(int userId);
        void SaveChanges();
    }
}