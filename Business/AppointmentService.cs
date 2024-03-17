using CliniCareApp.Models;
using CliniCareApp.Data;
using System;

namespace CliniCareApp.Business
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IAppointmentRepository _repository;

         public AppointmentService(IAppointmentRepository repository)
        {
            _repository = repository;
        }

        public void CreateAppointment(string patientDni, DateTime createdAt, string area, string medicalName, string date, string time, bool isUrgent)
        {
            var patient = _repository.GetPatientByDni(patientDni);

            if(patient != null)
            {
                var appointment = new Appointment(createdAt, area, medicalName, date, time, isUrgent, patient.Dni);

                _repository.AddAppointment(appointment);
                _repository.SaveChanges();
            }
        }

        public List<Appointment> GetAllAppointments()
        {
            return _repository.GetAllAppointments();
        }

        public Appointment GetAppointmentById(int appointmentId)
        {
            var appointment = _repository.GetAppointmentById(appointmentId);
            
            if(appointment == null)
            {
                  throw new KeyNotFoundException($"El paciente con Id {appointmentId} no existe.");
            }
            return appointment;
        }

        public List<Appointment> GetAppointments(string patientDni)
        {
           // Obtener todas las citas asociadas a un paciente por su DNI
            var appointments = _repository.GetAppointments(patientDni);

            if (appointments == null || appointments.Count == 0)
            {
                throw new KeyNotFoundException($"No hay citas para el paciente con DNI: {patientDni}");
            }
            return appointments;
        }

        public void UpdateAppointmentDetails(int appointmentId, AppointmentUpdateDTO appointmentUpdate)
        {
            var appointment = _repository.GetAppointmentById(appointmentId);

            if (appointment == null)
            {
                throw new KeyNotFoundException($"La cita con id: {appointmentId} no existe.");
            }

            appointment.Area = appointmentUpdate.Area;
            appointment.MedicalName = appointmentUpdate.MedicalName;
            appointment.Date = appointmentUpdate.Date;
            appointment.Time = appointmentUpdate.Time;
            appointment.IsUrgent = appointmentUpdate.IsUrgent;
            _repository.UpdateAppointment(appointment);
            _repository.SaveChanges();
        }

        public void DeleteAppointment(int appointmentId)
        {
            var patient = _repository.GetAppointmentById(appointmentId);

            if (patient == null)
            {
                throw new KeyNotFoundException($"La cita con Id: {appointmentId} no existe.");
            }
             _repository.DeleteAppointment(appointmentId);         
        }
    }
}