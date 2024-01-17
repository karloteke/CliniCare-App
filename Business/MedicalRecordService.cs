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

        public void CreateMedicalRecord(int patientId,DateTime medicalRecordDate, string doctorName, string treatment, decimal treatmentCost, string notes)
        {

            var patient = _repository.GetPatientById(patientId);

            if (patient != null)
            {
                var newMedicalRecord = new MedicalRecord(medicalRecordDate, doctorName, treatment, treatmentCost, notes)
                {
                    Patient = patient
                };

                _repository.AddMedicalRecord(newMedicalRecord);
                _repository.SaveChanges();
            }
        }

        public List<MedicalRecord> GetMedicalRecords()
        {
            return _repository.GetMedicalRecords();
        }
    }
}