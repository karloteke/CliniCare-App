namespace CliniCareApp.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


public class Appointment
{
    [Key]
    public int Id { get; set; }
    
    public DateTime CreatedAt { get; set; }  = DateTime.Now;

    [Required]
    public string? Area { get; set; }

    [Required]
    public string? MedicalName { get; set; }

    [Required]
    public string? Date { get; set; } 

    [Required]
    public string? Time { get; set; }

    [Required] 
    public bool IsUrgent { get; set; }  

    [ForeignKey("Patient")]
    public string? PatientDni { get; set; }


    public Appointment()
    {


    }

    
    private static int NextAppointmentId = 1;
    
    public Appointment (DateTime createdAt, string area, string medicalName, string date, string time, bool isUrgent, string patientDni)
    {
        // Id =  NextAppointmentId++;
        CreatedAt = createdAt;
        Area = area;
        MedicalName = medicalName;
        Date = date;
        Time = time;
        IsUrgent = isUrgent;
        PatientDni = patientDni;
    }  

    public static void UpdateNextAppointmentId(int nextId)
    {
        // NextAppointmentId = nextId;
    }
}




