using CliniCareApp.Models;

namespace CliniCareApp.Data
{
    public interface IPatientRepository
    {
        void AddPatient(Patient patient);
        List<Patient> GetPatients();
        Patient? GetPatientByDni(string? dni);
        Patient? GetPatientById (int? id);
        void UpdatePatient(Patient patient);
        void SaveChanges();
    }
}
