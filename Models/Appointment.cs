namespace ClinicApp.Models;

public class Appointment
{
    public int? Id { get; }
    public int PatientId { get; }
    public string? Area { get; set; }
    public string? MedicalName { get; set; }
    public string? Time { get; set; } 
    public string? Date { get; set; } 
    public bool IsUrgent { get; set; }  

    private static int NextId = 1;

    public Appointment (int patientId, string area, string medicalName,string time, string date, bool isUrgent)
    {
        PatientId = patientId;
        Area = area;
        MedicalName = medicalName;
        Time = time;
        Date = date;
        IsUrgent = isUrgent;

        Id = NextId;
        NextId++;
    }
}


