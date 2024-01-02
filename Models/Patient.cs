namespace CliniCareApp.Models;

public class Patient
{
    public int Id { get; private set; }
    public string? Name { get; set; }
    public string? LastName { get; set; }
    public string? Address { get; set; }
    public string? Dni { get; set; }
    public string? Phone { get; set; }

    private static int NextPatientId = 1;
    private  static List<Patient> Patients = new  List<Patient>();

    //Buscar paciente por Id
    public static Patient? GetPatientById(int patientId)
    {
        return Patients.FirstOrDefault(patient => patient.Id == patientId);
    }

    //Búsqueda paciente por Dni
    public static Patient? GetPatientByDni(string? dniToSearch)
    {
        return Patients.FirstOrDefault(patient => patient.Dni == dniToSearch);
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
}



    





