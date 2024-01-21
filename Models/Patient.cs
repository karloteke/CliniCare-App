namespace CliniCareApp.Models;

public class Patient
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? LastName { get; set; }
    public string? Address { get; set; }
    public string? Dni { get; set; }
    public string? Phone { get; set; }

    private static int NextPatientId = 1;

    public Patient()
    {
        Id = NextPatientId++;
    }

    public Patient(string? name, string? lastname, string? address, string? dni, string? phone)
    {
        Id = NextPatientId++;
        Name = name;
        LastName = lastname;
        Address =  address;
        Dni = dni;
        Phone = phone;
    }

    public static void UpdateNextPatientId(int nextId)
    {
        NextPatientId = nextId;
    }
}



    





