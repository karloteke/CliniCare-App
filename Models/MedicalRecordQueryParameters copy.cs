using System.ComponentModel.DataAnnotations;

namespace CliniCareApp.Models;

public class MedicalRecordQueryParameters
{
    public string? DoctorName { get; set; }
    public string? PatientDni { get; set; }
}

