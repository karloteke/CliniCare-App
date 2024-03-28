using CliniCareApp.Models;
using CliniCareApp.Data;

namespace CliniCareApp.Business
{
    public class PrivateAreaAccess
    {
        private readonly IUserService _userService;

        public PrivateAreaAccess(IUserService userService)
        {
            _userService = userService;
        }
        
        public User? Authentication(string userName, string password)
        {
            // Verifica si las credenciales son nulas o vacías
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
            {
                return null; 
            }

            // Utiliza el service de usuarios para autenticar
            User authenticatedUser = _userService.Authenticate(userName, password);

            if (authenticatedUser != null)
            {
                // Si el usuario es autenticado correctamente, devuelve el objeto User
                return authenticatedUser;
            }
            else
            {
                return null; 
            }
        }

    }
}




