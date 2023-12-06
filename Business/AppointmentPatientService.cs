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

        public void CreateAppointmentPatient()
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

            Console.WriteLine("Dni");
            string? dni = Console.ReadLine();
            Console.WriteLine("");

            Console.WriteLine("Teléfono");
            string? phone = Console.ReadLine();
            Console.WriteLine("");

            // Crear un nuevo paciente con los datos dados por consola
            var patient = new Patient(name, lastname, address, dni, phone);
 
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

                _repository.AddAppointmentPatient(newAppointmentPatient);
                _repository.SaveChanges();

                Console.WriteLine("");
                Console.WriteLine("CITA REGISTRADA CORRECTAMENTE");
            }
        }    

    
        public void ViewAppointmentPatient()
        {
            var appointmentPatients = _repository.GetAppointmentPatients();
            foreach (var appointmentPatient in appointmentPatients)
            {
                Console.WriteLine("");
                Console.WriteLine("---DATOS CITA---");
                Console.WriteLine("");
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