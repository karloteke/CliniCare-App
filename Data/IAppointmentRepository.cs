using CliniCareApp.Models;

namespace CliniCareApp.Data
{
    public interface IAppointmentRepository
    {
        void AddAppointment(Appointment appointment);
        List<Appointment> GetAppointments();
        Patient? GetPatientById(int? patientId);
        void UpdateAppointment(Appointment appointment);
        void SaveChanges();
    }
}
