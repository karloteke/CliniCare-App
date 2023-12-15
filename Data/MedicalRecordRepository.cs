using System.Text.Json;
using ClinicApp.Models;

namespace ClinicApp.Data
{
    public class MedicalRecordRepository : IMedicalRecordRepository
    {
        private List<MedicalRecord> _medicalRecords = new List<MedicalRecord>();
        //private List<Patient> _patients = new List<Patient>();
        private readonly string _filePath = "medicalRecords.json";
        private readonly IPatientRepository _patientRepository;

        public MedicalRecordRepository(IPatientRepository patientRepository)
        {
            _patientRepository = patientRepository;
            LoadMedicalRecords();
        }

        public void AddMedicalRecord(MedicalRecord medicalRecord)
        {
            _medicalRecords.Add(medicalRecord);
        }

        public List<MedicalRecord> GetMedicalRecords()
        {
            return _medicalRecords;
        }

        public Patient? GetPatientById(int? patientId)
        {
            return _patientRepository.GetPatientById(patientId);
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
        }
    }
}