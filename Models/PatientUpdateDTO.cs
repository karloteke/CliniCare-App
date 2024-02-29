using System.ComponentModel.DataAnnotations;
namespace CliniCareApp.Models; // CliniCare.DTO

public class PatientUpdateDTO
{
    [Required]
    [StringLength(30, ErrorMessage = "El nombre debe tener menos de 30 caracteres")]
    public string? Name { get; set; }

    [Required]
    [StringLength(30, ErrorMessage = "El apellido debe tener menos de 30 caracteres")]
    public string? LastName { get; set; }

    [Required]
    [StringLength(200, ErrorMessage = "La dirección debe tener menos de 200 caracteres")]
    public string? Address { get; set; }

    [Required(ErrorMessage = "El DNI es obligatorio.")]
    [RegularExpression(@"^\d{8}[a-zA-Z]$", ErrorMessage = "El DNI debe tener 8 dígitos seguidos de una letra.")]
    public string? Dni { get; set; }

    [Required(ErrorMessage = "El teléfono es obligatorio.")]
    [MinLength(9, ErrorMessage = "El teléfono debe tener al menos 9 caracteres.")]
    public string? Phone { get; set; }
}



