using CliniCareApp.Models;
using System.Collections.Generic; 

namespace CliniCareApp.Business
{
    public interface IPatientService
    {
        void CreatePatient(string name, string lastName, string address, string dni, string phone);
        List<Patient> ViewPatients(); 
        Patient? SearchByDni(string dni);
        Patient? GetPatientByDni(string dni);
        Patient? GetPatientById(int patientId);
    }
}