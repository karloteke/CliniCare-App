namespace ClinicApp.Models;

public class Appointment
{
    public int? Id { get; }
    public string? Area { get; set; }
    public string? MedicalName { get; set; }
    public string? Date { get; set; } 
    public string? Time { get; set; } 
    public bool IsUrgent { get; set; }  
    public Patient? Patient { get; set; }

    private static int NextIdA = 1;
    public List<Appointment> appointments = new List<Appointment>();

    public Appointment ( string area, string medicalName, string date, string time, bool isUrgent)
    {
        Id = NextIdA++;
        Area = area;
        MedicalName = medicalName;
        Date = date;
        Time = time;
        IsUrgent = isUrgent;
    }
}




