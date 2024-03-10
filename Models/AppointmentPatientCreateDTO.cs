using System.ComponentModel.DataAnnotations;


namespace CliniCareApp.Models; // CliniCare.DTO

public class AppointmentPatientCreateDTO
{
    public AppointmentCreateDTO? Appointment { get; set; }

    [Required(ErrorMessage = "Los datos del paciente son obligatorios.")]
    public PatientCreateDTO? Patient { get; set; }   
}