using Microsoft.AspNetCore.Mvc;

using CliniCareApp.Data;
using CliniCareApp.Business;
using CliniCareApp.Models;

namespace CliniCareApp.API.Controllers;

[ApiController]
[Route("[controller]")] 
public class UsersController : ControllerBase
{
    private readonly ILogger<UsersController> _logger;
    private readonly IUserService _userService;

    public UsersController(ILogger<UsersController> logger, IUserService UserService)
    {
        _logger = logger;
        _userService = UserService;
    }

    // GET: /Users
    [HttpGet(Name = "GetAllUsers")] 
    public ActionResult<IEnumerable<User>> GetUsers()
    {
        try 
        {
            var users = _userService.GetAllUsers();

            if(users.Any())
            {
            //Mapear cada usuario a un DTO de respuesta sin incluir la contraseña y la clave de acceso
            var userDtos = users.Select(user => new UserGetDTO //Iterar sobre la lista de usuarios 
            {
                UserName = user.UserName,
                Email = user.Email
            });

            return Ok(userDtos);
            }
            else
            {
                return NotFound("No existen usuarios para mostrar");
            }
        }     
        catch (Exception ex)
        {
            return BadRequest(ex);
        }      
    }

    // GET: /Users/{email}
    [HttpGet("{userEmail}", Name = "GetUserByEmail")]
    public IActionResult GetUserByEmail(string userEmail)
    {
        try
        {
            var user = _userService.GetUserByEmail(userEmail);

             // Mapeo el usuario a un DTO de respuesta sin incluir la contraseña y la clave de acceso
            var userDtoResponse = new UserGetDTO
            {
                UserName = user.UserName,
                Email = user.Email
            };

            return Ok(userDtoResponse);
            
        }
        catch (KeyNotFoundException)
        {
            return NotFound($"No existe usuario con este Email: {userEmail}");
        }
    }

    [HttpPost]
    public IActionResult NewUser([FromBody] UserCreateDTO userDto)
    {
        try 
        {
            // Verificar si el modelo recibido es válido
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var emailExist = _userService.GetUserByEmail(userDto.Email);
            if(emailExist != null)
            {
                return BadRequest("El correo electrónico ya está registrado.");
            }

            if(userDto.AccessKey != "medico")
            {
                return BadRequest("La clave de registro para registrarse como médico es incorrecta");
            }

            var user = _userService.CreateUser(userDto.UserName, userDto.Password, userDto.Email, userDto.AccessKey);
            return CreatedAtAction(nameof(GetUsers), new { UserName = user.UserName }, userDto);
        }     
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    //PUT: /Users/{email}
    [HttpPut("{userEmail}")]
    public IActionResult UpdateUser(string userEmail, [FromBody] UserUpdateDTO userDto)
    {
        if (!ModelState.IsValid)  {return BadRequest(ModelState); } 

        if(userDto.AccessKey != "medico")
            {
                return BadRequest("La clave de registro para registrarse como médico es incorrecta");
            }

        try
        {
            _userService.UpdateUserDetails(userEmail, userDto);
            return Ok($"El usuario con Email: {userEmail} ha sido actualizado correctamente");
        }
        catch (KeyNotFoundException)
        {
           return NotFound();
        }
    }

    // DELETE: /User/{UserEmail}
    [HttpDelete("{userEmail}")]
    public IActionResult DeleteUser(string userEmail )
    {
        try
        {
            _userService.DeleteUser(userEmail);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            _logger.LogInformation(ex.Message);
            return NotFound($"El email: {userEmail} no coincide con ningun usuario regitrado");
        }
    }
}
