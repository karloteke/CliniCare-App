using System.ComponentModel.DataAnnotations;

namespace CliniCareApp.Models;

public class AppointmentQueryParameters
{
    public string? Area { get; set; }

    public string? MedicalName { get; set; }
}

