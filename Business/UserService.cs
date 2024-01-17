using CliniCareApp.Models;
using CliniCareApp.Data;


namespace CliniCareApp.Business
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;

        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }
        public void CreateUser(string inputUserName, string inputPassword, string inputEmail, string inputAccessKey)
        {
            var newUser = new User(inputUserName, inputPassword, inputEmail, inputAccessKey);
        
            _repository.AddUser(newUser);
            _repository.SaveChanges();
        }

        public User GetUserByUserName(string inputUserName)
        {
            return _repository.GetUserByUserName(inputUserName);
        }
    
        public bool Authenticate(string userName, string password)
        {
            // Verifica las credenciales del usuario y devuelve true si son válidas, false en caso contrario
            User? user = _repository.GetUserByUserName(userName);
            return user != null && user.Password == password;
        }

    }
}