namespace CliniCareApp.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class MedicalRecord
{
    [Key]
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }  = DateTime.Now;

    [Required]
    public string? DoctorName { get; set; }

    [Required]
    public string? Treatment { get; set; } 

    public decimal? TreatmentCost { get; set; }

    public string? Notes { get; set; } 

    [ForeignKey("Patient")]
    public string? PatientDni { get; set; }

   
    private static int NextMedicalRecordId = 1;

    public MedicalRecord() 
    {
        // Constructor vacío para la deserialización
    }

    public MedicalRecord(DateTime createdAt, string doctorName, string treatment, decimal treatmentCost, string notes, string patientDni)
    {
        // Id =  NextMedicalRecordId++;
        CreatedAt = createdAt;
        DoctorName = doctorName;
        Treatment = treatment;
        TreatmentCost = treatmentCost;
        Notes = notes;
        PatientDni = patientDni;
    }
    
    public static void UpdateNextMedicalRecordId(int nextId)
    { 
        // NextMedicalRecordId = nextId;
    }
}


