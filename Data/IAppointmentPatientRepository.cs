using ClinicApp.Models;

namespace ClinicApp.Data
{
    public interface IAppointmentPatientRepository
    {
        void AddAppointmentPatient(AppointmentPatient appointmentPatient);
        List<AppointmentPatient> GetAppointmentPatients();
        Patient? GetPatientById(int? patientId);
        void UpdateAppointmentPatient(AppointmentPatient appointmentPatient);
        void SaveChanges();
    }
}