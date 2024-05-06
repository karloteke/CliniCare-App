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

        public void CreateAppointmentPatient(Patient patient,string area, string day, string time, bool isUrgent)
        {
            DateTime createdAt = DateTime.Now;
            var newAppointmentPatient = new AppointmentPatient(createdAt, area, day, time, isUrgent, patient)
            {
                Patient = patient // Asigna el paciente al objeto AppointmentPatient
            };

            _repository.AddAppointmentPatient(newAppointmentPatient);
            _repository.SaveChanges();

        }    

        public Patient? GetPatientById(int patientId)
        {
            var patient = _repository.GetPatientById(patientId);

            if(patient == null)
            {
                throw new KeyNotFoundException($"El paciente con Id {patientId} no existe.");
            }
            return patient;
        }

        // public List<AppointmentPatient> GetAllAppointmentPatients()
        // {
        //     return _repository.GetAllAppointmentPatients();
        // }

        public List<AppointmentPatient> GetAppointmentPatientsByDNI(string dni)
        {
            return _repository.GetAppointmentPatientsByDNI(dni);
        }
    }
}