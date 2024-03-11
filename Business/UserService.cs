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
        public User CreateUser(string userName, string password, string email, string accessKey)
        {
            var user = new User(userName, password, email, accessKey);
        
            _repository.AddUser(user);
            _repository.SaveChanges();
            return user;
        }

        public List<User> GetAllUsers()
        {
            return _repository.GetUsers();
        }  

        public User? GetUserByUserName(string userName)
        {
            return _repository.GetUserByUserName(userName);
        }

        public User? GetUserByEmail(string userEmail)
        {
            return _repository.GetUserByEmail(userEmail);
        }

        public void NewUser(UserCreateDTO userCreate)
        {

            var newUser = new User
            (
                userCreate.UserName,
                userCreate.Password,
                userCreate.Email,
                userCreate.AccessKey
            );

            _repository.AddUser(newUser);
            _repository.SaveChanges();
        }

        public void UpdateUserDetails(string userEmail, UserUpdateDTO userUpdate)
        {
            var user = _repository.GetUserByEmail(userEmail);
            if (user == null)
            {
                throw new KeyNotFoundException($"El usuario cuyo Email es: {userEmail} no existe.");
            }

            user.UserName = userUpdate.UserName;
            user.Password = userUpdate.Password;
            user.Email = userUpdate.Email;
            user.AccessKey = userUpdate.AccessKey;
            _repository.UpdateUser(user);
            _repository.SaveChanges();
        }

        public void DeleteUser(string userEmail)
        {
            var user = _repository.GetUserByEmail(userEmail);
            if (user == null)
            {
                throw new KeyNotFoundException($"El usuario con Email: {userEmail} no existe.");
            }
             _repository.DeleteUser(userEmail);         
        }
    
        public bool Authenticate(string userName, string password)
        {
            // Verifica las credenciales del usuario y devuelve true si son válidas, false en caso contrario
            User? user = _repository.GetUserByUserName(userName);
            return user != null && user.Password == password;
        }

    }
}