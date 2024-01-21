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

        public void CreateAppointment(int patientId, DateTime appointmentDate, string area, string medicalName, string date, string time, bool isUrgent)
        {
            var patient = _repository.GetPatientById(patientId);

            if(patient != null)
            {
                var newAppointment = new Appointment(appointmentDate, area, medicalName, date, time, isUrgent, patient.Id);

                _repository.AddAppointment(newAppointment);
                _repository.SaveChanges();
            }
        }

        public List<Appointment> GetAppointments()
        {
            return _repository.GetAppointments();
        }
    }
}