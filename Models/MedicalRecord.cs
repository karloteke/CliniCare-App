namespace CliniCareApp.Models;
public class MedicalRecord
{
    public int Id { get; private set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public string? DoctorName { get; set; }
    public string? Treatment { get; set; } 
    public decimal? TreatmentCost { get; set; }
    public string? Notes { get; set; } 
    public Patient? Patient { get; set; }

    private static int NextMedicalRecordId = 1;
    public List <MedicalRecord> medicalRecords = new List<MedicalRecord>();

     // Constructor sin parámetros para la deserialización
    public MedicalRecord() { }

    public MedicalRecord(DateTime createdAt, string doctorName, string treatment, decimal treatmentCost, string notes)
    {
        Id =  NextMedicalRecordId++;
        CreatedAt = createdAt;
        DoctorName = doctorName;
        Treatment = treatment;
        TreatmentCost = treatmentCost;
        Notes = notes;
    }
}


