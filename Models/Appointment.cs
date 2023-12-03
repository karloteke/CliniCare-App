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

    private static int NextId = 1;
    private static readonly List<Appointment> appointments = new List<Appointment>();


    public Appointment ( string area, string medicalName, string date, string time, bool isUrgent)
    {
        Id = NextId;
        Area = area;
        MedicalName = medicalName;
        Date = date;
        Time = time;
        IsUrgent = isUrgent;
        NextId++;
    }

    public static void CreateAppointment()
    {
        Console.WriteLine("Especialidad");
        string? area = Console.ReadLine();
        Console.WriteLine("");

        Console.WriteLine("Nombre del médico");
        string? medicalName = Console.ReadLine();
        Console.WriteLine("");

       
        Console.WriteLine("Hora (HH:mm)");
        string? time = Console.ReadLine();
        Console.WriteLine("");
        
 
        Console.WriteLine("Fecha (dd/MM/yyyy)");
        string? date = Console.ReadLine();
        Console.WriteLine("");

        bool isUrgent;

        Console.WriteLine("¿Es urgente? (si/no)");
        string? isUrgentInput = Console.ReadLine();
  
        if(isUrgentInput == "no")
        {
            isUrgent = false;
        }
        else if(isUrgentInput == "si")
        {
            isUrgent = true;
        }
        else
        {
            Console.WriteLine("Respuesta incorrecta");
            return;
        }

        Console.WriteLine ("");
        Console.WriteLine ("Introduce el ID del paciente al que quieres asignarle la cita");

        int PatientId;

        if(!int.TryParse(Console.ReadLine(),out PatientId))
        {
            Console.WriteLine("ID de paciente no válido. Debe ser un número entero.");
            return;
        }
            var patient = Patient.GetPatientById(PatientId);

        if(patient != null)
        {
            if (area != null && medicalName != null && date != null && time != null)
            {
                var newAppointment = new Appointment(area, medicalName, date, time, isUrgent)
                {
                    Patient = patient
                };
                appointments.Add(newAppointment);

                Console.WriteLine("");
                Console.WriteLine($"CITA REGISTRADA CORRECTAMENTE PARA: {patient.Name} {patient.LastName}");
            }
        }
        else
        {
            Console.WriteLine($"No se encontró al paciente con ID {PatientId}");
        }
    }
    
    public static void ViewAppointment()
    {
        foreach (var appointment in appointments)
        {
            Console.WriteLine("---DATOS CITA---");
            Console.WriteLine("");
            Console.WriteLine($"Id: {appointment.Id}");
            Console.WriteLine($"Paciente: {appointment.Patient?.Name} {appointment.Patient?.LastName}");
            Console.WriteLine($"Especialidad: {appointment.Area}");
            Console.WriteLine($"Nombre médico: {appointment.MedicalName}");
            Console.WriteLine($"Hora: {appointment.Time}");
            Console.WriteLine($"Día: {appointment.Date}");
            Console.WriteLine($"¿Es ugente?: {appointment.IsUrgent}");
            Console.WriteLine(""); 
        }
    }

}


