using System.Text.Json;
using CliniCareApp.Models;

namespace CliniCareApp.Data
{
    public class PatientRepository : IPatientRepository
    {
        private List<Patient> _patients = new List<Patient>();
        private readonly string _filePath;

        public PatientRepository()
        {
            _filePath = GetFilePath();
            LoadPatients();
        }

        private string GetFilePath()
        {
            string basePath = Environment.GetEnvironmentVariable("IS_DOCKER") == "true" ? "SharedFolder" : "";
            return Path.Combine(basePath, "patients.json");
        }

        public void AddPatient(Patient patient)
        {
            //Verifica si ya existe el paciente en la lista antes de agregarlo
            // if (!_patients.Any(p => p.Id == patient.Id))
            // {
                _patients.Add(patient);
                SaveChanges();
            // }
        }

       public IEnumerable<Patient> GetAllPatients(PatientQueryParameters? patientQueryParameters, bool orderByNameAsc) {
        var query = _patients.AsQueryable();

        
        if (!string.IsNullOrWhiteSpace(patientQueryParameters.Dni)) 
        {
            query = query.Where(p => p.Dni.Contains(patientQueryParameters.Dni));
        }

        if (!string.IsNullOrWhiteSpace(patientQueryParameters.Name)) 
        {
            query = query.Where(p => p.Name.Contains(patientQueryParameters.Name));
        }

        if (!string.IsNullOrWhiteSpace(patientQueryParameters.LastName)) 
        {
            query = query.Where(p => p.LastName.Contains(patientQueryParameters.LastName));
        }

        if (orderByNameAsc) 
        {
            query = query.OrderBy(p => p.Name);
        }

        var result = query.ToList();
        return result;
    }


        public List<Patient> GetPatients()
        {
            return _patients;
        }

        public Patient? GetPatientByDni(string? dni)
        {
            return _patients.FirstOrDefault(p => p.Dni == dni);
        }

        public Patient? GetPatientById(int? patientId)
        {
            return _patients.FirstOrDefault(p => p.Id == patientId);
        }

        public void UpdatePatient(Patient patient)
        {
            AddPatient(patient);
        }

        public void DeletePatient(int? patientId)
        {
            if (patientId != null)
            {
                var patient = _patients.FirstOrDefault(p => p.Id == patientId);
                if (patient != null)
                {
                    _patients.Remove(patient);
                    SaveChanges();
                }
            }
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

                if (_patients.Any()) 
                {
                    // int maxId = _patients.Max(p => p.Id);
                    // Patient.UpdateNextPatientId(maxId + 1); 
                }  
              
            }           
    
        }
    }
}
