using ClinicApp.Models;

namespace ClinicApp.Business
{
    public interface IAppointmentPatientService
    {
        void CreateAppointmentPatient(Patient patient);
        void ViewAppointmentPatient(string patientDni);    
    }
}