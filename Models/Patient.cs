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
    private static readonly List<Patient> patients = new  List<Patient>();

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

    public static void SearchByDni(){
        
        Console.WriteLine("Ingrese el DNI del paciente a buscar:");
        string? dniToSearch = Console.ReadLine();
        Console.WriteLine("");
        
        Patient? foundPatient = GetPatientByDni(dniToSearch);
        if (foundPatient != null)
        {
            Console.WriteLine("--- DATOS DE PACIENTE ENCONTRADO ---");
            Console.WriteLine("");
            Console.WriteLine($"Id: {foundPatient.Id}");
            Console.WriteLine($"Nombre: {foundPatient.Name}");
            Console.WriteLine($"Apellido: {foundPatient.LastName}");
            Console.WriteLine($"Dirección: {foundPatient.Address}");
            Console.WriteLine($"Dni: {foundPatient.Dni}");
            Console.WriteLine($"Teléfono: {foundPatient.Phone}");
        }
        else
        {
            Console.WriteLine("Paciente no encontrado.");
        }
    }
}

    





