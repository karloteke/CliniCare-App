using CliniCareApp.Models;
using CliniCareApp.Data;
using System;

namespace CliniCareApp.Business
{
    public class AppointmentPatientService : IAppointmentPatientService
    {
        private readonly IAppointmentPatientRepository _repository;

        public AppointmentPatientService(IAppointmentPatientRepository repository)
        {
            _repository = repository;
        }

        public void CreateAppointmentPatient(string patientName, string patientLastName, string patientAddress, string patientDni, string patientPhone,string area, string day, string time, bool isUrgent)
        {
            DateTime createdAt = DateTime.Now;
            var newAppointmentPatient = new AppointmentPatient(createdAt, area, day, time, isUrgent, patientName, patientLastName, patientDni, patientAddress, patientPhone);

            _repository.AddAppointmentPatient(newAppointmentPatient);
            _repository.SaveChanges();

        }    

        public List<AppointmentPatient> GetAppointmentPatientsByDNI(string dni)
        {
            return _repository.GetAppointmentPatientsByDNI(dni);
        }
    }
}