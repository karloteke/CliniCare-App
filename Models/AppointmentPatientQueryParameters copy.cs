using System.ComponentModel.DataAnnotations;

namespace CliniCareApp.Models;

public class AppointmentPatientQueryParameters
{
    [Required]
    public string? PatientDni { get; set; }

}

