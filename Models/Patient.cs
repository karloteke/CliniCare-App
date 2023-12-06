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
    private  static List<Patient> patients = new  List<Patient>();

    //Buscar paciente por Id
    public static Patient? GetPatientById(int patientId)
    {
        return patients.FirstOrDefault(patient => patient.Id == patientId);
    }

    //Búsqueda paciente por Dni
    public static Patient? GetPatientByDni(string? dniToSearch)
    {
        return patients.FirstOrDefault(patient => patient.Dni == dniToSearch);
    }

    public Patient(string? name, string? lastname, string? address, string? dni, string? phone)
    {
        Id = NextId++;
        Name = name;
        LastName = lastname;
        Address =  address;
        Dni = dni;
        Phone = phone;
    }
}



    





