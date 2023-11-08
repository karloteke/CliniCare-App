namespace ClinicApp.Models;
public class MedicalRecord
{
    public int Id { get; }
    public int PatientId { get; } 
    public DateTime date { get; }
    public string? Treatment { get; set; } 
    public decimal? TreatmentCost { get; set; }
    public string? Notes { get; set; } 
    
}


