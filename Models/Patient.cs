namespace ClinicApp.Models;

public class Patient
{
    public int Id { get; }
    public string? Name { get; set; }
    public string? LastName { get; set; }
    public string? Address { get; set; }
    public string? Dni { get; set; }
    public string? Phone { get; set; }

    private static int NextId = 1;
    public Patient(string? name, string? lastname, string? address, string? dni, string? phone)
    {
        Name = name;
        LastName = lastname;
        Address =  address;
        Dni = dni;
        Phone = phone;

        Id = NextId;
        NextId++;
    }
}








