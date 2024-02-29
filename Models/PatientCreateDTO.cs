using System.ComponentModel.DataAnnotations;

namespace CliniCareApp.Models; // CliniCare.DTO

public class PatientCreateDTO
{
    [Required(ErrorMessage = "El nombre es obligatorio.")]
    public string? Name { get; set; }

    [Required(ErrorMessage = "El apellido es obligatorio.")]
    public string? LastName { get; set; }

    [Required(ErrorMessage = "La dirección es obligatoria.")]
    public string? Address { get; set; }

    [Required(ErrorMessage = "El DNI es obligatorio.")]
    [RegularExpression(@"^\d{8}[a-zA-Z]$", ErrorMessage = "El DNI debe tener 8 dígitos seguidos de una letra.")]
    public string? Dni { get; set; }

    [Required(ErrorMessage = "El teléfono es obligatorio.")]
    [MinLength(9, ErrorMessage = "El teléfono debe tener al menos 9 caracteres.")]
    public string? Phone { get; set; }
}
