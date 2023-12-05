using System.Text.Json;
using ClinicApp.Models;

namespace ClinicApp.Data
{
    public class PatientRepository : IPatientRepository
    {
        private List<Patient> _patients = new List<Patient>();
        private readonly string _filePath = "patients.json";

        public PatientRepository()
        {
            LoadPatients();
        }

        public void AddPatient(Patient patient)
        {
            _patients.Add(patient);
        }

        public List<Patient> GetPatients()
        {
            return _patients;
        }

        public Patient GetPatientByDni(string dni)
        {
            return _patients.FirstOrDefault(p => p.Dni == dni);
        }

        public Patient GetPatientById(int patientId)
        {
            return _patients.FirstOrDefault(p => p.Id == patientId);
        }

        public void UpdatePatient(Patient patient)
        {
            AddPatient(patient);
        }

        public void SaveChanges()
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            string jsonString = JsonSerializer.Serialize(_patients, options);
            File.WriteAllText(_filePath, jsonString);
        }

        private void LoadPatients()
        {
            if (File.Exists(_filePath))
            {
                string jsonString = File.ReadAllText(_filePath);
                var patients = JsonSerializer.Deserialize<List<Patient>>(jsonString);
                _patients = patients ?? new List<Patient>();
            }
        }
    }
}
