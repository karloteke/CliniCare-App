using CliniCareApp.Models;

namespace CliniCareApp.Business
{
    public interface IAppointmentPatientService
    {
        void CreateAppointmentPatient(Patient patient);
        void ViewAppointmentPatient(string patientDni);    
    }
}