namespace CliniCareApp.Models;
public class MedicalRecord
{
    public int Id { get; private set; }
    public DateTime CreatedAt { get; set; }
    public string? DoctorName { get; set; }
    public string? Treatment { get; set; } 
    public decimal? TreatmentCost { get; set; }
    public string? Notes { get; set; } 
    public Patient? Patient { get; set; }

    private static int NextMedicalRecordId = 1;

    public MedicalRecord() 
    {
        Id = NextMedicalRecordId++;
    }

    public MedicalRecord(DateTime createdAt, string doctorName, string treatment, decimal treatmentCost, string notes)
    {
        Id =  NextMedicalRecordId++;
        CreatedAt = createdAt;
        DoctorName = doctorName;
        Treatment = treatment;
        TreatmentCost = treatmentCost;
        Notes = notes;
    }

    public static void UpdateNextMedicalRecordId(int nextId)
    {
        NextMedicalRecordId = nextId;
    }
}


