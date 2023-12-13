using ClinicApp.Models;
using ClinicApp.Data;


namespace ClinicApp.Business
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;

        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }
        public void CreateUser()
        {
            Console.WriteLine("Introduce un nombre de usuario");
            string? userName = Console.ReadLine();
            Console.WriteLine("");

            Console.WriteLine("Introduce una contraseña");
            string? password = Console.ReadLine();
            Console.WriteLine("");

            if (!password.Any(char.IsUpper) || (!password.Any(char.IsDigit)))
            {
                Console.WriteLine("Formato de contraseña inválido. Debe contener al menos una mayúscula y un número.");
                return;
            }

            Console.WriteLine("Introduce un email");
            string? email = Console.ReadLine();
            Console.WriteLine("");
            
            if (!email.Contains('@') || !email.Contains(".com"))
            {
                Console.WriteLine("Formato de email inválido. Debe contener '@' y '.com'.");
                return;
            }

            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(email) )
            {
                Console.WriteLine("Entrada inválida. Nombre de usuario, contraseña e email no pueden estar vacíos.");
                return;
            }

            var newUser = new User(userName, password, email);
            var existUser = _repository.GetUserByUserName(userName);

            if (existUser == null)
            {
                _repository.AddUser(newUser);
                _repository.SaveChanges();

                Console.WriteLine("USUARIO REGISTRADO CORRECTAMENTE");
            }
            else
            {
                Console.WriteLine("USUARIO NO REGISTRADO, YA EXISTE ESE NOMBRE");
            }
        }
    
        public bool Authenticate(string username, string password)
        {
            // Verifica las credenciales del usuario y devuelve true si son válidas, false en caso contrario
            User? user = _repository.GetUserByUserName(username);
            return user != null && user.Password == password;
        }

        public bool Authenticate(string username, string password, string email)
        {
            // Verifica las credenciales del usuario y devuelve true si son válidas, false en caso contrario
            User? user = _repository.GetUserByUserName(username);
            return user != null && user.Password == password && user.Email == email;
        }
    }
}