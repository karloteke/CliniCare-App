using CliniCareApp.Models;

namespace CliniCareApp.Data
{
    public interface IMedicalRecordRepository
    {
        void AddMedicalRecord(MedicalRecord medicalRecord);
        List<MedicalRecord> GetAllMedicalRecords();
        public IEnumerable<MedicalRecord> GetAllMedicalRecords(MedicalRecordQueryParameters? medicalRecordQueryParameters);
        Patient? GetPatientById(int? patientId);
        Patient? GetPatientByDni(string? patientDni);
        MedicalRecord GetMedicalRecordById(int medicalRecordId);
        List<MedicalRecord> GetMedicalRecords(string patientDni);
        void UpdateMedicalRecord(MedicalRecord medicalRecord);
        void DeleteMedicalRecord(int? id);
        void SaveChanges();
    }
}
