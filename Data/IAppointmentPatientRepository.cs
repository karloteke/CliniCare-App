using ClinicApp.Models;

namespace ClinicApp.Data
{
    public interface IAppointmentPatientRepository
    {
        void AddAppointmentPatient(AppointmentPatient appointmentPatient);
        List<AppointmentPatient> GetAppointmentPatients();
        List<AppointmentPatient> GetAppointmentPatientsByDNI(string dni);
        void UpdateAppointmentPatient(AppointmentPatient appointmentPatient);
        void SaveChanges();
    }
}