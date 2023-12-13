using ClinicApp.Models;

namespace ClinicApp.Data
{
    public interface IUserRepository
    {
        void AddUser(User user);
        User? GetUserByUserName(string userName);
        void UpdateUser(User user);
        void SaveChanges();
    }
}