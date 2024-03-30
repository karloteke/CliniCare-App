using CliniCareApp.Models;

namespace CliniCareApp.Data
{
    public interface IPatientRepository
    {
        void AddPatient(Patient patient);
        public IEnumerable<Patient> GetAllPatients(PatientQueryParameters? patientQueryParameters, bool orderByNameAsc);
        List<Patient> GetPatients();
        Patient? GetPatientByDni(string? dni);
        Patient? GetPatientById (int? id);
        void UpdatePatient(Patient patient);
        void DeletePatient(int? id);
        void SaveChanges();
    }
}
