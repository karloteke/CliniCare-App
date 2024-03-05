using System.ComponentModel.DataAnnotations;

namespace CliniCareApp.Models; // CliniCare.DTO

public class MedicalRecordUpdateDTO
{
    [Required(ErrorMessage = "El nombre del médico es obligatorio.")]
    public string? DoctorName { get; set; }
    
    [Required(ErrorMessage = "El tratamiento es obligatorio.")]
    public string? Treatment { get; set; } 
    public decimal TreatmentCost { get; set; }
    public string? Notes { get; set; } 
}