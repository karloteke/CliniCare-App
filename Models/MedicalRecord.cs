
namespace ClinicApp.Models;
public class MedicalRecord
{
    public int Id { get; }
    public DateTime Date { get; }
    public string? DoctorName { get; set; }
    public string? Treatment { get; set; } 
    public decimal? TreatmentCost { get; set; }
    public string? Notes { get; set; } 
    public Patient? Patient { get; set; }

    private static int NextId = 1;
    private static readonly List <MedicalRecord> medicalRecords = new List<MedicalRecord>();


    public MedicalRecord(DateTime date, string doctorName, string treatment, decimal treatmentCost, string notes)
    {
        Id = NextId;
        Date = date;
        DoctorName = doctorName;
        Treatment = treatment;
        TreatmentCost = treatmentCost;
        Notes = notes;
        NextId++;
    }
    
    public static void CreateMedicalRecords()
    {
        DateTime date = DateTime.Now;
        Console.WriteLine($"Fecha y hora: {date}");

        Console.WriteLine("Nombre del médico:");
        string? doctorName = Console.ReadLine();

        
        Console.WriteLine("Tratamiento:");
        string? treatment = Console.ReadLine();

        Console.WriteLine("Coste del tratamiento (Ingrese un número decimal):");
        string? treatmentCostInput = Console.ReadLine();

        if (decimal.TryParse(treatmentCostInput, out decimal treatmentCost)&& treatmentCost % 1 != 0){        
        }
        else
        {
            Console.WriteLine("Valor no válido.Ingrese número decimal.");
            return;
        }
    
        Console.WriteLine("Notas:");
        string? notes = Console.ReadLine();

        Console.WriteLine("");
        Console.WriteLine("Introduce el ID del paciente al que quieres asignarle la cita");

        int PatientId;

        if (!int.TryParse(Console.ReadLine(), out PatientId))
        {
            Console.WriteLine("ID de paciente no válido. Debe ser un número entero.");
            return;
        }

        var patient = Patient.GetPatientById(PatientId);

        if (patient != null)
        {
            if (doctorName != null && treatment != null && notes != null)
            {
                var newMedicalRecord = new MedicalRecord(date, doctorName, treatment, treatmentCost, notes)
                {
                    Patient = patient
                };
                medicalRecords.Add(newMedicalRecord);

                Console.WriteLine("");
                Console.WriteLine($"HISTORIAL MÉDICO REGISTRADO CORRECTAMENTE PARA: {patient.Name} {patient.LastName}");
            }
        }
        else
        {
            Console.WriteLine($"No se encontró al paciente con ID {PatientId}");
        }
    }

    public static void ViewMedicalRecords(){
        foreach (var medicalRecord in medicalRecords)
        {
            Console.WriteLine("---DATOS HISTORIAL MÉDICO---");
            Console.WriteLine("");
            Console.WriteLine($"Id: {medicalRecord.Id}");
            Console.WriteLine($"Paciente: {medicalRecord.Patient?.Name} {medicalRecord.Patient?.LastName}");
            Console.WriteLine($"Fecha y hora: {medicalRecord.Date}");
            Console.WriteLine($"Nombre médico: {medicalRecord.DoctorName}");
            Console.WriteLine($"Tratamiento: {medicalRecord.Treatment}");
            Console.WriteLine($"Coste del tratamiento: {medicalRecord.TreatmentCost}");
            Console.WriteLine($"Notas: {medicalRecord.Notes}");
            Console.WriteLine(""); 
        }
    }
}


