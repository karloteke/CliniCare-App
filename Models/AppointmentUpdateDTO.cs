using System.ComponentModel.DataAnnotations;

namespace CliniCareApp.Models; // CliniCare.DTO

public class AppointmentUpdateDTO
{
    [Required(ErrorMessage = "El area es obligatoria.")]
    public string? Area { get; set; }

    [Required(ErrorMessage = "El nombre del médico es obligatorio.")]
    public string? MedicalName { get; set; }

    [Required(ErrorMessage = "La fecha es obligatoria.")]
    [RegularExpression(@"^(0[1-9]|[12][0-9]|3[01])/(0[1-9]|1[0-2])/(20)\d{2}$", ErrorMessage = "La fecha debe estar en el formato dd/MM/yyyy")]
    public string? Date { get; set; }

    [Required(ErrorMessage = "La hora es obligatoria.")]
    [RegularExpression(@"^(0[1-9]|1[0-2]):[0-5][0-9]$", ErrorMessage = "El formato de hora debe ser hh:mm.")]
    public string? Time { get; set; }

    [Required(ErrorMessage = "La urgencia es obligatoria.")]
    public bool IsUrgent { get; set; }  
}
