namespace ClinicApp.Business
{
    public interface IUserService
    {
        void CreateUser();
        bool Authenticate(string username, string password);
        bool Authenticate(string username, string password, string email);
    }
}
