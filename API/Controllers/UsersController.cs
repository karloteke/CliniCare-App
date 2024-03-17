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
        
    [HttpGet(Name = "GetAllUsers")] 
    public ActionResult<IEnumerable<User>> SearchUsers(string? userName)
    {
        var query = _userService.GetAllUsers().AsQueryable();

        if (!string.IsNullOrWhiteSpace(userName))
        {
            query = query.Where(u => u.UserName.Contains(userName));
        }

        var users = query.ToList();
        
        //Mapear cada usuario a un DTO de respuesta sin incluir la contraseña y la clave de acceso
        var userDtos = users.Select(user => new UserGetDTO 
        {
            UserName = user.UserName,
            Email = user.Email
        });

        if (users.Count == 0)
        {
            return NotFound();
        }

        return Ok(userDtos);
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

            var emailExist = _userService.GetUserByUserName(userDto.UserName);
            if(emailExist != null)
            {
                return BadRequest("El usuario ya está registrado.");
            }

            if(userDto.AccessKey != "medico")
            {
                return BadRequest("La clave de registro para registrarse como médico es incorrecta");
            }

            var user = _userService.CreateUser(userDto.UserName, userDto.Password, userDto.Email, userDto.AccessKey);
            return CreatedAtAction(nameof(SearchUsers), new { UserName = user.UserName }, userDto);
        }     
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    //PUT: /Users/{userName}
    [HttpPut("{userUserName}")]
    public IActionResult UpdateUser(string userUserName, [FromBody] UserUpdateDTO userDto)
    {
        if (!ModelState.IsValid)  {return BadRequest(ModelState); } 

        if(userDto.AccessKey != "medico")
            {
                return BadRequest("La clave de registro para registrarse como médico es incorrecta");
            }

        try
        {
            _userService.UpdateUserDetails(userUserName, userDto);
            return Ok($"El usuario con usuario: {userUserName} ha sido actualizado correctamente");
        }
        catch (KeyNotFoundException)
        {
           return NotFound();
        }
    }

    // DELETE: /User/{UserName}
    [HttpDelete("{userUserName}")]
    public IActionResult DeleteUser(string userUserName)
    {
        try
        {
            _userService.DeleteUser(userUserName);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            _logger.LogInformation(ex.Message);
            return NotFound($"El usuario: {userUserName} no existe");
        }
    }
}
