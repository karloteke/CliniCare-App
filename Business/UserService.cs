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
        public void CreateUser()
        {
            Console.WriteLine("Introduce un nombre de usuario");
            string? userName = Console.ReadLine();
            Console.WriteLine("");

            if(string.IsNullOrEmpty(userName))
            {
                Console.WriteLine("Entrada inválida. Nombre de usuario no puede estar vacío.");
                return;
            }

            var existUser = _repository.GetUserByUserName(userName);
            if (existUser != null)
            {
                Console.WriteLine("YA EXISTE ESE NOMBRE DE USUARIO");
                return;
            }

            Console.WriteLine("Introduce una contraseña");
            string? password = Console.ReadLine();
            Console.WriteLine("");

            if(password != null)
            {
                if (!password.Any(char.IsUpper) || (!password.Any(char.IsDigit)))
                {
                    Console.WriteLine("Formato de contraseña inválido. Debe contener al menos una mayúscula y un número.");
                    return;
                }
            }
           
            Console.WriteLine("Introduce la clave de acceso para médicos:");
            string? accessKey = Console.ReadLine();
            Console.WriteLine("");

            // Validar la clave de acceso para médicos
            if (string.Equals(accessKey, "medico"))
            {
              
            }
            else
            {
                Console.WriteLine("Clave de acceso incorrecta o vacia. Registro denegado.");
                return;
            }

            Console.WriteLine("Introduce un email");
            string? email = Console.ReadLine();
            Console.WriteLine("");
            if(email != null)
            {
                 if (!email.Contains('@') || !email.Contains(".com"))
                {
                    Console.WriteLine("Formato de email inválido. Debe contener '@' y '.com'.");
                    return;
                }
            }
            
            var newUser = new User(userName, password, email, accessKey);
        
                _repository.AddUser(newUser);
                _repository.SaveChanges();

                Console.WriteLine("USUARIO REGISTRADO CORRECTAMENTE");
        }
    
        public bool Authenticate(string username, string password)
        {
            // Verifica las credenciales del usuario y devuelve true si son válidas, false en caso contrario
            User? user = _repository.GetUserByUserName(username);
            return user != null && user.Password == password;
        }

    }
}