using Microsoft.AspNetCore.Mvc;
using CliniCareApp.Business;
using CliniCareApp.Models;
using Microsoft.AspNetCore.Authorization; 

namespace CliniCareApp.API.Controllers;

[ApiController]
[Route("[controller]")] 
[Authorize]
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

        if (users.Count == 0)
        {
            return NotFound();
        }

        return users;
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

            var user = _userService.CreateUser(userDto.UserName, userDto.Password, userDto.Email);
            return CreatedAtAction(nameof(SearchUsers), new { userId = user.Id }, userDto);
        }     
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    //PUT: /Users/{id}
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
