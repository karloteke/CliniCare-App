using CliniCareApp.Models;
using System.Collections.Generic; 


namespace CliniCareApp.Business
{
    public interface IAppointmentPatientService
    {
        void CreateAppointmentPatient(string patientName, string patientLastName, string patientAddress, string patientDni, string patientPhone,string area, string day, string time, bool isUrgent);  
        List<AppointmentPatient> GetAppointmentPatientsByDNI(string dni);
    }
}