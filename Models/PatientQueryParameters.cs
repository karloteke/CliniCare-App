using System.ComponentModel.DataAnnotations;

namespace CliniCareApp.Models;

public class PatientQueryParameters
{
    public string? Name { get; set; }

    public string? LastName { get; set; }

    [RegularExpression(@"^\d{8}[a-zA-Z]$", ErrorMessage = "El DNI debe tener 8 dígitos seguidos de una letra.")]
    public string? Dni { get; set; }
}

