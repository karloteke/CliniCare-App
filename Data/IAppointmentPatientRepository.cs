using CliniCareApp.Models;

namespace CliniCareApp.Data
{
    public interface IAppointmentPatientRepository
    {
        void AddAppointmentPatient(AppointmentPatient appointmentPatient);
        // List<AppointmentPatient> GetAllAppointmentPatients();
        List<AppointmentPatient> GetAppointmentPatientsByDNI(string dni);
        List<Patient> GetPatients();
        Patient? GetPatientById(int? patientId);
        // AppointmentPatient GetAppointmentPatientById(int appointmentPatientId);
        Patient? GetPatientByDni(string? dni);
        void UpdateAppointmentPatient(AppointmentPatient appointmentPatient);
        void SaveChanges();
    }
}