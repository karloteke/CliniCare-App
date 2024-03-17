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

        public void CreateMedicalRecord(string patientDni, DateTime medicalRecordDate, string doctorName, string treatment, decimal treatmentCost, string notes)
        {
            var patient = _repository.GetPatientByDni(patientDni);

            if (patient != null)
            {
                var newMedicalRecord = new MedicalRecord(medicalRecordDate, doctorName, treatment, treatmentCost, notes, patient.Dni);

                _repository.AddMedicalRecord(newMedicalRecord);
                _repository.SaveChanges();
            }
        }


        public List<MedicalRecord> GetAllMedicalRecords()
        {
            return _repository.GetAllMedicalRecords();
        }
        
        public MedicalRecord GetMedicalRecordById(int medicalRecordId)
        {
            var appointment = _repository.GetMedicalRecordById(medicalRecordId);
            
            if(appointment == null)
            {
                  throw new KeyNotFoundException($"El historial con Id {medicalRecordId} no existe.");
            }
            return appointment;
        }

        public List<MedicalRecord> GetMedicalRecords(string patientDni)
        {
           // Obtener todasel historial asociado a un paciente por su DNI
            var medicalRecords = _repository.GetMedicalRecords(patientDni);

            if (medicalRecords == null || medicalRecords.Count == 0)
            {
                throw new KeyNotFoundException($"No hay citas para el paciente con DNI: {patientDni}");
            }
            return medicalRecords;
        }

        public void UpdateMedicalRecordDetails(int medicalRecordId, MedicalRecordUpdateDTO medicalRecordUpdate)
        {
            var medicalRecord = _repository.GetMedicalRecordById(medicalRecordId);

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