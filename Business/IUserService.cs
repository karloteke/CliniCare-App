namespace CliniCareApp.Business
{
    public interface IUserService
    {
        void CreateUser();
        bool Authenticate(string username, string password);
    }
}
