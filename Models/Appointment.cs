namespace CliniCareApp.Models;

public class Appointment
{
    public int? Id { get; private set; }
    public DateTime CreatedAt { get; set; } 
    public string? Area { get; set; }
    public string? MedicalName { get; set; }
    public string? Date { get; set; } 
    public string? Time { get; set; } 
    public bool IsUrgent { get; set; }  
    public Patient? Patient { get; set; }

    private static int NextAppointmentId = 1;
    
    public Appointment (DateTime createdAt, string area, string medicalName, string date, string time, bool isUrgent)
    {
        Id =  NextAppointmentId++;
        CreatedAt = createdAt;
        Area = area;
        MedicalName = medicalName;
        Date = date;
        Time = time;
        IsUrgent = isUrgent;
    }  
}




