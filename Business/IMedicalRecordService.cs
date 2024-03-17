using System.Collections.Generic; 
using CliniCareApp.Models; 

namespace CliniCareApp.Business
{
    public interface IMedicalRecordService
    {
        void CreateMedicalRecord(string patientDni, DateTime medicalRecordDate, string doctorName, string treatment, decimal treatmentCost, string notes); 
        List<MedicalRecord> GetAllMedicalRecords(); 
        MedicalRecord GetMedicalRecordById(int medicalRecordId);  
        List<MedicalRecord> GetMedicalRecords(string patientDni);
        public void UpdateMedicalRecordDetails(int medicalRecordId, MedicalRecordUpdateDTO medicalRecordUpdateDTO);
        public void DeleteMedicalRecord(int medicalRecordId);
    }
}