namespace CliniCareApp.Models;

public class MedicalRecord
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public string? DoctorName { get; set; }
    public string? Treatment { get; set; } 
    public decimal? TreatmentCost { get; set; }
    public string? Notes { get; set; } 
    public string? PatientDni { get; set; }
    private static int NextMedicalRecordId = 1;

    public MedicalRecord() 
    {
        // Constructor vacío para la deserialización
    }

    public MedicalRecord(DateTime createdAt, string doctorName, string treatment, decimal treatmentCost, string notes, string patientDni)
    {
        Id =  NextMedicalRecordId++;
        CreatedAt = createdAt;
        DoctorName = doctorName;
        Treatment = treatment;
        TreatmentCost = treatmentCost;
        Notes = notes;
        PatientDni = patientDni;
    }
    
    public static void UpdateNextMedicalRecordId(int nextId)
    { 
        NextMedicalRecordId = nextId;
    }
}


