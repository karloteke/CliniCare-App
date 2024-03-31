using CliniCareApp.Models;
using Microsoft.EntityFrameworkCore;

namespace CliniCareApp.Data
{
    public class MedicalRecordEFRepository : IMedicalRecordRepository
    {
        private readonly CliniCareContext _context;

        public MedicalRecordEFRepository(CliniCareContext context)
        {
            _context = context;
        }

        public void AddMedicalRecord(MedicalRecord medicalRecord)
        {
            _context.MedicalRecords.Add(medicalRecord);
        }
        
        public IEnumerable<MedicalRecord> GetAllMedicalRecords(MedicalRecordQueryParameters? medicalRecordQueryParameters) {
        var query = _context.MedicalRecords.AsQueryable();

        
            if (!string.IsNullOrWhiteSpace(medicalRecordQueryParameters.DoctorName)) {
                query = query.Where(m => m.DoctorName.Contains(medicalRecordQueryParameters.DoctorName));
            }

            if (!string.IsNullOrWhiteSpace(medicalRecordQueryParameters.PatientDni)) {
                query = query.Where(m => m.PatientDni.Contains(medicalRecordQueryParameters.PatientDni));
            }  

            var result = query.ToList();
            return result;
        }
        
        public List<MedicalRecord> GetAllMedicalRecords()
        {
            return _context.MedicalRecords.ToList();
        }

        public Patient? GetPatientById(int? patientId)
        {
            return _context.Patients.FirstOrDefault(p => p.Id == patientId);
        }

        public Patient? GetPatientByDni(string? patientDni)
        {
            return _context.Patients.FirstOrDefault(p => p.Dni == patientDni);
        }

        public MedicalRecord GetMedicalRecordById(int medicalRecordId)
        {
            return _context.MedicalRecords.FirstOrDefault(m => m.Id == medicalRecordId);
        }


        public List<MedicalRecord> GetMedicalRecords(string patientDni)
        {
            return _context.MedicalRecords.ToList();
        }

        public void UpdateMedicalRecord(MedicalRecord medicalRecord)
        {
            _context.Entry(medicalRecord).State = EntityState.Modified;
        }

        public void DeleteMedicalRecord(int? medicalRecordId)
        {
            var medicalRecord = _context.MedicalRecords.FirstOrDefault(m => m.Id == medicalRecordId);
            if (medicalRecord != null)
            {
                _context.MedicalRecords.Remove(medicalRecord);
                _context.SaveChanges();
            }
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
