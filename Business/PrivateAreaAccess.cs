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

        public bool Authentication(string userName, string password)
        {
            // Utiliza el servicio de usuarios para autenticar
            return _userService.Authenticate(userName, password);
        }
    }
}




