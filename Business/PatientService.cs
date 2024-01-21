using CliniCareApp.Models;
using CliniCareApp.Data;


namespace CliniCareApp.Business
{
    public class PatientService : IPatientService
    {
        private readonly IPatientRepository _repository;

        public PatientService(IPatientRepository repository)
        {
            _repository = repository;
        }
        public void CreatePatient(string name, string lastName, string address, string dni, string phone)
        {
            var newPatient = new Patient(name, lastName, address, dni, phone);
            _repository.AddPatient(newPatient);
        }    

        public List<Patient> ViewPatients()
        {
            return _repository.GetPatients();
        }  
    
        public Patient? SearchByDni(string dni)
        {
            return _repository.GetPatientByDni(dni);
        }

        public Patient? GetPatientById(int patientId)
        {
            return _repository.GetPatientById(patientId);
        }

          public Patient? GetPatientByDni(string dni)
        {
            return _repository.GetPatientByDni(dni);
        }
    }
}