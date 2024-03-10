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

        public Patient CreatePatient(string name, string lastName, string address, string dni, string phone)
        {
            var patient = new Patient(name, lastName, address, dni, phone);
            _repository.AddPatient(patient);
            _repository.SaveChanges();
            return patient;
        }

        public List<Patient> GetAllPatients()
        {
            return _repository.GetPatients();
        }  
    
        public Patient? SearchByDni(string dni)
        {
            return _repository.GetPatientByDni(dni);
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

        public Patient? GetPatientByDni(string dni)
        {
            return _repository.GetPatientByDni(dni);
        }

        public void NewPatient(PatientCreateDTO patientCreate)
        {
            // Crear un nuevo paciente utilizando los datos del objeto PatientCreateDTO
            var newPatient = new Patient
            {
                Name = patientCreate.Name,
                LastName = patientCreate.LastName,
                Address = patientCreate.Address,
                Dni = patientCreate.Dni,
                Phone = patientCreate.Phone
            };

            _repository.AddPatient(newPatient);
            _repository.SaveChanges();
        }

        public void UpdatePatientDetails(int patientId, PatientUpdateDTO patientUpdate)
        {
            var patient = _repository.GetPatientById(patientId);
            if (patient == null)
            {
                throw new KeyNotFoundException($"El paciente con id: {patientId} no existe.");
            }

            patient.Name = patientUpdate.Name;
            patient.LastName = patientUpdate.LastName;
            patient.Address = patientUpdate.Address;
            patient.Dni = patientUpdate.Dni;
            patient.Phone = patientUpdate.Phone;
            _repository.UpdatePatient(patient);
            _repository.SaveChanges();
        }

        public void DeletePatient(int patientId)
        {
            var patient = _repository.GetPatientById(patientId);
            if (patient == null)
            {
                throw new KeyNotFoundException($"El paciente con Id: {patientId} no existe.");
            }
             _repository.DeletePatient(patientId);         
        }

    }
}