using CliniCareApp.Models;
using System.Collections.Generic; 

namespace CliniCareApp.Business
{
    public interface IPatientService
    {
        Patient? CreatePatient(string name, string lastName, string address, string dni, string phone);
        List<Patient> GetAllPatients(); 
        public IEnumerable<Patient> GetAllPatients(PatientQueryParameters? patientQueryParameters, bool orderByNameAsc);
        Patient? SearchByDni(string dni);
        Patient? GetPatientByDni(string dni);
        Patient? GetPatientById(int patientId);
        public void UpdatePatientDetails(int patientId, PatientUpdateDTO PatientUpdate);
        public void DeletePatient(int patientId);
    }
}