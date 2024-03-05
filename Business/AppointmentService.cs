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

        public void CreateAppointment(int patientId, DateTime createdAt, string area, string medicalName, string date, string time, bool isUrgent)
        {
            var patient = _repository. GetPatientById(patientId);

            if(patient != null)
            {
                var newAppointment = new Appointment(createdAt, area, medicalName, date, time, isUrgent, patient.Id);

                _repository.AddAppointment(newAppointment);
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