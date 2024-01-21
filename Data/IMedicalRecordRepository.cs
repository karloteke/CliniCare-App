using CliniCareApp.Models;

namespace CliniCareApp.Data
{
    public interface IMedicalRecordRepository
    {
        void AddMedicalRecord(MedicalRecord medicalRecord);
        List<MedicalRecord> GetMedicalRecords();
        Patient? GetPatientById(int? patientId);
        void UpdateMedicalRecord(MedicalRecord medicalRecord);
        void SaveChanges();
    }
}
