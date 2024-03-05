using CliniCareApp.Models;

namespace CliniCareApp.Data
{
    public interface IMedicalRecordRepository
    {
        void AddMedicalRecord(MedicalRecord medicalRecord);
        List<MedicalRecord> GetAllMedicalRecords();
        Patient? GetPatientById(int? patientId);
        MedicalRecord GetMedicalRecordById(int medicalRecordId);
        void UpdateMedicalRecord(MedicalRecord medicalRecord);
        void DeleteMedicalRecord(int? id);
        void SaveChanges();
    }
}
