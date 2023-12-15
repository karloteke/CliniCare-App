using ClinicApp.Models;
using ClinicApp.Data;
using System;

namespace ClinicApp.Business
{
    public class AppointmentPatientService : IAppointmentPatientService
    {
        private readonly IAppointmentPatientRepository _repository;

         public AppointmentPatientService(IAppointmentPatientRepository repository)
        {
            _repository = repository;
        }

        public void CreateAppointmentPatient(Patient patient)
        {
            Console.WriteLine("Especialidad (Oftalmología/traumatología/ginecología/neurología)");
            string? area = Console.ReadLine();
            Console.WriteLine("");

            if (string.IsNullOrEmpty(area))
            {
                Console.WriteLine("Entrada inválida. La especialidad no puede estar vacía.");
                return;
            }

            Console.WriteLine("Fecha (dd/MM/yyyy)");
            string? day = Console.ReadLine();
            Console.WriteLine("");

            if(day != null)
            {
                if (FechaValida(day))
                {
            
                }
                else
                {
                    Console.WriteLine("Fecha inválida. Debe tener el formato dd/MM/yyyy.");
                    return;
                }   
            }
            else
            {
                Console.WriteLine("Entrada inválida. La fecha no puede estar vacía.");
                return;
            }
            
            Console.WriteLine("Hora (HH:mm)");
            string? time = Console.ReadLine();
            Console.WriteLine("");

            if(time != null)
            {
                if (HoraValida(time))
                {
            
                }
                else
                {
                    Console.WriteLine("Hora inválida. Debe tener el formato HH:mm.");
                    return;
                }   
            }
            else
            {
                Console.WriteLine("Entrada inválida. La hora no puede estar vacía.");
                return;
            }

    
            //Comprobación entradas fecha y hora
            static bool HoraValida(string hora)
            {
                return DateTime.TryParseExact(hora, "HH:mm", null, System.Globalization.DateTimeStyles.None, out _);
            }

            static bool FechaValida(string fecha)
            {
                return DateTime.TryParseExact(fecha, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out _);
            }

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
                Console.WriteLine("Respuesta incorrecta. Escribe si o no");
                return;
            }

            Console.WriteLine("");
            DateTime date = DateTime.Now;
            Console.WriteLine($"Fecha y hora de registro: {date}");

            var newAppointmentPatient = new AppointmentPatient(date, area, day, time, isUrgent)
            {
                Patient = patient
            };

            _repository.AddAppointmentPatient(newAppointmentPatient);
            _repository.SaveChanges();

            Console.WriteLine("");
            Console.WriteLine("CITA REGISTRADA CORRECTAMENTE");
        }    

    
        public void ViewAppointmentPatient(string patientDni)
        {
            var appointmentPatients = _repository.GetAppointmentPatientsByDNI(patientDni);
            foreach (var appointmentPatient in appointmentPatients)
            {
                Console.WriteLine("");
                Console.WriteLine("---DATOS CITA---");
                Console.WriteLine("");
                Console.WriteLine($"Id cita: {appointmentPatient.Id}");
                Console.WriteLine($"Paciente: {appointmentPatient.Patient?.Name} {appointmentPatient.Patient?.LastName} con id {appointmentPatient.Patient?.Id}");
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