using ClinicApp.Models;
using ClinicApp.Data;
using System;

namespace ClinicApp.Business
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IAppointmentRepository _repository;

         public AppointmentService(IAppointmentRepository repository)
        {
            _repository = repository;
        }

        public void CreateAppointment()
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

            
            var patient = _repository.GetPatientById(PatientId);

            if(patient != null)
            {
                if (area != null && medicalName != null && date != null && time != null)
                {
                    var newAppointment = new Appointment(area, medicalName, date, time, isUrgent)
                    {
                        Patient = patient
                    };

                    _repository.AddAppointment(newAppointment);
                    _repository.SaveChanges();

                    Console.WriteLine("");
                    Console.WriteLine($"CITA REGISTRADA CORRECTAMENTE PARA: {newAppointment.Patient?.Name} {newAppointment.Patient?.LastName}");
                }
            }
            else
            {
                Console.WriteLine($"No se encontró al paciente con ID {PatientId}");
            }
        }
    
        public void ViewAppointment()
        {
            var appointments = _repository.GetAppointments();

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
                Console.WriteLine($"¿Es urgente?: {(appointment.IsUrgent ? "si" : "no")}");
                Console.WriteLine(""); 
            }    
        }
    }
}