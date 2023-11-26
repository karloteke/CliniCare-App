
namespace ClinicApp.Models;

public class Patient
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? LastName { get; set; }
    public string? Address { get; set; }
    public string? Dni { get; set; }
    public string? Phone { get; set; }

    private static int NextId = 1;
    private static readonly List<Patient> patients = new  List<Patient>();

    public static Patient? GetPatientById(int patientId) //función dentro de la clase Patient que busca un paciente en la lista patients según su ID
    {
        return patients.FirstOrDefault(patient => patient.Id == patientId);//busca el primer elemento en la lista patients por id y si existe devuelve el paciente
    }
    public Patient(string? name, string? lastname, string? address, string? dni, string? phone)
    {
        Id = NextId;
        Name = name;
        LastName = lastname;
        Address =  address;
        Dni = dni;
        Phone = phone;
        NextId++;
    }

    public static void CreatePatient()
    {       
        Console.WriteLine("Nombre");
        string? name = Console.ReadLine();
        Console.WriteLine("");

        Console.WriteLine("Apellido");
        string? lastname = Console.ReadLine();
        Console.WriteLine("");

        Console.WriteLine("Dirección");
        string? address = Console.ReadLine();
        Console.WriteLine("");

        Console.WriteLine("DNI");
        string? dni = Console.ReadLine();
        Console.WriteLine("");

        Console.WriteLine("Teléfono");
        string? phone = Console.ReadLine();
        Console.WriteLine("");

        var newPatient = new Patient(name, lastname, address, dni, phone);
        patients.Add(newPatient);
        
        Console.WriteLine($"PACIENTE REGISTRADO CORRECTAMENTE");
        
    }

    public static void ViewPatients(){    
        {
            if (patients.Count == 0)
            {
                Console.WriteLine("NO EXISTE NINGÚN PACIENTE EN LA LISTA");
            }
            else
            {
                foreach (var patient in patients)
                {
                    Console.WriteLine("---DATOS PACIENTE---");
                    Console.WriteLine("");
                    Console.WriteLine($"Id: {patient.Id}");
                    Console.WriteLine($"Nombre: {patient.Name}");
                    Console.WriteLine($"Apellido: {patient.LastName}");
                    Console.WriteLine($"Dirección: {patient.Address}");
                    Console.WriteLine($"Dni: {patient.Dni}");
                    Console.WriteLine($"Teléfono: {patient.Phone}");
                    Console.WriteLine("");

                }
            }
        }
    }

}
    





