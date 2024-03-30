namespace CliniCareApp.Models;
using System.ComponentModel.DataAnnotations;
public class Patient
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string? Name { get; set; }

    [Required]
    public string? LastName { get; set; }

    [Required]
    public string? Address { get; set; }

    [Required]
    public string? Dni { get; set; }

    [Required]
    public string? Phone { get; set; }


    private static int NextPatientId = 1;

    public Patient()
    {
        // Id = NextPatientId++;
    }

    public Patient(string? name, string? lastname, string? address, string? dni, string? phone)
    {
        // Id = NextPatientId++;
        Name = name;
        LastName = lastname;
        Address =  address;
        Dni = dni;
        Phone = phone;
    }

    public static void UpdateNextPatientId(int nextId)
    {
        // NextPatientId = nextId;
    }
}



    





