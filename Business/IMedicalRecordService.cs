using System.Collections.Generic; 
using CliniCareApp.Models; 

namespace CliniCareApp.Business
{
    public interface IMedicalRecordService
    {
        void CreateMedicalRecord(int patientId, DateTime medicalRecordDate, string doctorName, string treatment, decimal treatmentCost, string notes); 
        List<MedicalRecord> GetAllMedicalRecords(); 
        MedicalRecord GetMedicalRecordById(int medicalRecordId);
        public void UpdateMedicalRecordDetails(int medicalRecordId, MedicalRecordUpdateDTO medicalRecordUpdateDTO);
        public void DeleteMedicalRecord(int medicalRecordId);
    }
}