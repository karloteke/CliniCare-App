using CliniCareApp.Models;
using System.Collections.Generic; 


namespace CliniCareApp.Business
{
    public interface IAppointmentPatientService
    {
        void CreateAppointmentPatient(Patient patient,string area, string day, string time, bool isUrgent);
        // List<AppointmentPatient> GetAllAppointmentPatients();  
        Patient? GetPatientById(int patientId);
        List<AppointmentPatient> GetAppointmentPatientsByDNI(string dni);
    }
}