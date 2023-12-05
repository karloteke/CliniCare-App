
namespace ClinicApp.Models;
public class MedicalRecord
{
    public int Id { get; }
    public DateTime Date { get; }
    public string? DoctorName { get; set; }
    public string? Treatment { get; set; } 
    public decimal? TreatmentCost { get; set; }
    public string? Notes { get; set; } 
    public Patient? Patient { get; set; }

    private static int NextId = 1;
    private static readonly List <MedicalRecord> medicalRecords = new List<MedicalRecord>();


    public MedicalRecord(DateTime date, string doctorName, string treatment, decimal treatmentCost, string notes)
    {
        Id = NextId++;
        Date = date;
        DoctorName = doctorName;
        Treatment = treatment;
        TreatmentCost = treatmentCost;
        Notes = notes;
    }

    public static void AddMedicalRecord(MedicalRecord newMedicalRecord)
    {
        medicalRecords.Add(newMedicalRecord);
    }

    public static List<MedicalRecord> GetMedicalRecord()
    {
        return medicalRecords;
    }
}


