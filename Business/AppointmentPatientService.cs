using ClinicApp.Models;
using System;

namespace ClinicApp.Business
{
    public class AppointmentPatientService : IAppointmentPatientService
    {
        // Crea una instancia específica para la lista de citas.
        List<AppointmentPatient> appointmentsPatient = AppointmentPatient.GetAppointments();

        public void CreateAppointmentPatient()
        {
            Console.WriteLine("Nombre");
            string? name = Console.ReadLine();
            Console.WriteLine("");

            Console.WriteLine("Apellido");
            string? lastname = Console.ReadLine();
            Console.WriteLine("");

          // Crear un nuevo paciente con los datos proporcionados
            var patient = new Patient(name, lastname, null, null, null);
 
            Console.WriteLine("Especialidad (Oftalmología, traumatología, ginecología o neurología)");
            string? area = Console.ReadLine();
            Console.WriteLine("");

            Console.WriteLine("Fecha (dd/MM/yyyy)");
            string? day = Console.ReadLine();
            Console.WriteLine("");

            Console.WriteLine("Hora (HH:mm)");
            string? time = Console.ReadLine();
            Console.WriteLine("");

            bool isUrgent;

            Console.WriteLine("¿Es urgente? (si/no)");
            string? isUrgentInput = Console.ReadLine();

            if (isUrgentInput == "no")
            {
                isUrgent = false;
            }
            else if (isUrgentInput == "si")
            {
                isUrgent = true;
            }
            else
            {
                Console.WriteLine("Respuesta incorrecta");
                return;
            }

            Console.WriteLine("");
            DateTime date = DateTime.Now;
            Console.WriteLine($"Fecha y hora de registro: {date}");

            if (area != null && day != null && time != null)
            {
                var newAppointmentPatient = new AppointmentPatient(date, area, day, time, isUrgent)
                {
                    Patient = patient
                };

                appointmentsPatient.Add(newAppointmentPatient);

                Console.WriteLine("");
                Console.WriteLine("CITA REGISTRADA CORRECTAMENTE");
            }
        }    

    
        public void ViewAppointmentPatient()
        {
            foreach (var appointmentPatient in appointmentsPatient)
            {
                Console.WriteLine("");
                Console.WriteLine("---DATOS CITA---");
                Console.WriteLine("");
                Console.WriteLine($"Id: {appointmentPatient.Id}");
                Console.WriteLine($"Paciente: {appointmentPatient.Patient?.Name} {appointmentPatient.Patient?.LastName}");
                Console.WriteLine($"Fecha y hora de registro: {appointmentPatient.Date}");
                Console.WriteLine($"Especialidad: {appointmentPatient.Area}");
                Console.WriteLine($"Fecha de la cita: {appointmentPatient.Day}");
                Console.WriteLine($"Hora de la cita: {appointmentPatient.Time}");
                Console.WriteLine($"¿Es urgente?: {(appointmentPatient.IsUrgent ? "si" : "no")}");
                Console.WriteLine(""); 
            }
        }
    }
}