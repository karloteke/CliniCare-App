using System.Collections.Generic;
using CliniCareApp.Models;
using CliniCareApp.Data;
using System;

namespace CliniCareApp.Business
{
    public class MedicalRecordService : IMedicalRecordService
    {
        private readonly IMedicalRecordRepository _repository;

        public MedicalRecordService(IMedicalRecordRepository repository)
        {
            _repository = repository;
        }

        public void CreateMedicalRecord(int patientId, DateTime medicalRecordDate, string doctorName, string treatment, decimal treatmentCost, string notes)
        {
            var patient = _repository.GetPatientById(patientId);

            if (patient != null)
            {
                var newMedicalRecord = new MedicalRecord(medicalRecordDate, doctorName, treatment, treatmentCost, notes, patient.Id);

                _repository.AddMedicalRecord(newMedicalRecord);
                _repository.SaveChanges();
            }
        }

        public List<MedicalRecord> GetAllMedicalRecords()
        {
            return _repository.GetAllMedicalRecords();
        }

        public void UpdateMedicalRecordDetails(int medicalRecordId, MedicalRecordUpdateDTO medicalRecordUpdate)
        {
            var medicalRecord = _repository.GetMedicalRecordById( medicalRecordId);

            if (medicalRecord == null)
            {
                throw new KeyNotFoundException($"La cita con id: { medicalRecordId} no existe.");
            }

            medicalRecord.DoctorName = medicalRecordUpdate.DoctorName;
            medicalRecord.Treatment = medicalRecordUpdate.Treatment;
            medicalRecord.TreatmentCost = medicalRecordUpdate.TreatmentCost;
            medicalRecord.Notes = medicalRecordUpdate.Notes;
            _repository.UpdateMedicalRecord(medicalRecord);
            _repository.SaveChanges();
        }

        public MedicalRecord GetMedicalRecordById(int medicalRecordId)
        {
            var medicalRecord = _repository.GetMedicalRecordById(medicalRecordId);
            
            if(medicalRecord == null)
            {
                  throw new KeyNotFoundException($"El historial médico con Id: {medicalRecordId} no existe.");
            }
            return medicalRecord;
        }

        public void DeleteMedicalRecord(int medicalRecordId)
        {
            var medicalRecord = _repository.GetMedicalRecordById(medicalRecordId);

            if (medicalRecord == null)
            {
                throw new KeyNotFoundException($"El historial médico con Id: {medicalRecordId} no existe.");
            }
             _repository.DeleteMedicalRecord(medicalRecordId);         
        }
    }
}