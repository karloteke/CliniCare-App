using CliniCareApp.Models;
using System.Collections.Generic; 

namespace CliniCareApp.Business
{
    public interface IPatientService
    {
        Patient? CreatePatient(PatientCreateDTO patientDto);
        List<Patient> GetAllPatients(); 
        Patient? SearchByDni(string dni);
        Patient? GetPatientByDni(string dni);
        Patient? GetPatientById(int patientId);
        public void UpdatePatientDetails(int patientId, PatientUpdateDTO PatientUpdate);
        public void DeletePatient(int patientId);

    }
}