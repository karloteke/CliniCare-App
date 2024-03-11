using System.ComponentModel.DataAnnotations;

public class UserGetDTO
{   
    [Required(ErrorMessage = "El campo de usuario es obligatorio.")]
    public string? UserName { get; set; }

    [Required(ErrorMessage = "El campo de correo electrónico es obligatorio.")]
    [EmailAddress(ErrorMessage = "El correo electrónico no tiene un formato válido.")]
    public string? Email { get; set; }

}
