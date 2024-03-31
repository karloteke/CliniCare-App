using System.Text.Json;
using CliniCareApp.Models;

namespace CliniCareApp.Data
{
    public class MedicalRecordRepository : IMedicalRecordRepository
    {
        private List<MedicalRecord> _medicalRecords = new List<MedicalRecord>();
        private List<Patient> _patients = new List<Patient>();
        private readonly string _filePath;
        private readonly IPatientRepository _patientRepository;

        public MedicalRecordRepository(IPatientRepository patientRepository)
        {
            _patientRepository = patientRepository;
            _filePath = GetFilePath();
            LoadMedicalRecords();
        }

        private string GetFilePath()
        {
            string basePath = Environment.GetEnvironmentVariable("IS_DOCKER") == "true" ? "SharedFolder" : "";
            return Path.Combine(basePath, "medicalRecords.json");
        }

        public void AddMedicalRecord(MedicalRecord medicalRecord)
        {
            //Verifico si existe el historial médico en la lista antes de agregarlo
            if(!_medicalRecords.Any(m => m.Id == medicalRecord.Id ) )
            {
                _medicalRecords.Add(medicalRecord);
                SaveChanges();
            }
        }

                
        public IEnumerable<MedicalRecord> GetAllMedicalRecords(MedicalRecordQueryParameters? medicalRecordQueryParameters)
        {
            var query = _medicalRecords.AsQueryable();


            if (!string.IsNullOrWhiteSpace(medicalRecordQueryParameters.DoctorName)) 
            {
                query = query.Where(m => m.DoctorName.Contains(medicalRecordQueryParameters.DoctorName));
            }

            if (!string.IsNullOrWhiteSpace(medicalRecordQueryParameters.PatientDni)) 
            {
                query = query.Where(m => m.PatientDni.Contains(medicalRecordQueryParameters.PatientDni));
            }

            var result = query.ToList();
            return result;
        }


        public List<MedicalRecord> GetAllMedicalRecords()
        {
            return _medicalRecords;
        }

        public Patient? GetPatientById(int? patientId)
        {
            return _patientRepository.GetPatientById(patientId);
        }

        public MedicalRecord GetMedicalRecordById(int medicalRecordId)
        {
            return _medicalRecords.FirstOrDefault(m => m.Id == medicalRecordId);
        }

        public Patient? GetPatientByDni(string patientDni)
        {
            return _patientRepository.GetPatientByDni(patientDni);
        }

        public List<MedicalRecord> GetMedicalRecords(string patientDni)
        {
            // Implementación para obtener todas las citas asociadas a un paciente por su DNI
            return _medicalRecords.Where(mr => mr.PatientDni == patientDni).ToList();
        }
        public void DeleteMedicalRecord(int? medicalRecordId)
        {
            if (medicalRecordId != null)
            {
                var medicalRecord = _medicalRecords.FirstOrDefault(m => m.Id == medicalRecordId);
                if (medicalRecord != null)
                {
                    _medicalRecords.Remove(medicalRecord);
                    SaveChanges();
                }
            }
        }

        public void UpdateMedicalRecord(MedicalRecord medicalRecord)
        {
            AddMedicalRecord(medicalRecord);
        }

        public void SaveChanges()
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            string jsonString = JsonSerializer.Serialize(_medicalRecords, options);
            File.WriteAllText(_filePath, jsonString);
        }

        private void LoadMedicalRecords()
        {
            if (File.Exists(_filePath))
            {         
                string jsonString = File.ReadAllText(_filePath);  
                var medicalRecords = JsonSerializer.Deserialize<List<MedicalRecord>>(jsonString);
                _medicalRecords = medicalRecords ?? new List<MedicalRecord>();
            }

            if (_medicalRecords.Any())
            {
                int maxId = _medicalRecords.Max(mr => mr.Id);
                MedicalRecord.UpdateNextMedicalRecordId(maxId + 1);
            }
        }
    }
}