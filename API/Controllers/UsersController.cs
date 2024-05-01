using Microsoft.AspNetCore.Mvc;
using CliniCareApp.Business;
using CliniCareApp.Models;
using Microsoft.AspNetCore.Authorization; 

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
        
    [Authorize(Roles = Roles.Admin)]
    [HttpGet(Name = "GetAllUsers")] 
    public ActionResult<IEnumerable<User>> GetAllUsers([FromQuery] UserQueryParameters userQueryParameters)
    {
        if (!ModelState.IsValid)  {return BadRequest(ModelState); } 

        try 
        {
            var patients = _userService.GetAllUsers(userQueryParameters);
            
                if (patients == null || !patients.Any())
                    {
                        return NotFound("No hay pacientes disponibles.");
                    }
                    
            return Ok(patients);
        }     
        catch (Exception ex)
        {
            return BadRequest(ex);
        }
    }

    [Authorize(Roles = "admin, user")]
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

            if (string.IsNullOrEmpty(userDto.UserName) || string.IsNullOrEmpty(userDto.Password) || string.IsNullOrEmpty(userDto.Email))
            {
                return BadRequest("Los campos no pueden estar vacíos.");
            }

            var userExist = _userService.GetUserByUserName(userDto.UserName);
            if (userExist != null)
            {
                return BadRequest("El usuario ya está registrado.");
            }

            var user = _userService.CreateUser(userDto.UserName, userDto.Password, userDto.Email);

            // Retornar la acción exitosa junto con el nuevo usuario creado
            return CreatedAtAction(nameof(GetAllUsers), new { userId = user.Id }, userDto);
        }     
        catch (Exception ex)
        {
            // Si ocurre un error, retornar un BadRequest con el mensaje de error
            return BadRequest(ex.Message);
        }
    }

    
    //PUT: /Users/{id}
    [Authorize(Roles = Roles.Admin)]
    [HttpPut("{userId}")]
    public IActionResult UpdateUser(int userId, [FromBody] UserUpdateDTO userDto)
    {
        if (!ModelState.IsValid)  {return BadRequest(ModelState); } 

        try
        {
            _userService.UpdateUserDetails(userId, userDto);
            return Ok($"El usuario con Id: {userId} ha sido actualizado correctamente");
        }
        catch (KeyNotFoundException)
        {
           return NotFound();
        }
    }

    // DELETE: /User/{Userid}
    [Authorize(Roles = Roles.Admin)]
    [HttpDelete("{userId}")]
    public IActionResult DeleteUser(int userId)
    {
        try
        {
            _userService.DeleteUser(userId);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            _logger.LogInformation(ex.Message);
            return NotFound($"El usuario con Id: {userId} no existe");
        }
    }
}
