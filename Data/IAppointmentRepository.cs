using CliniCareApp.Models;

namespace CliniCareApp.Data
{
    public interface IAppointmentRepository
    {
        void AddAppointment(Appointment appointment);
        List<Appointment> GetAllAppointments();
        Patient? GetPatientById(int? patientId);
        Appointment GetAppointmentById(int appointmentId);
        void UpdateAppointment(Appointment appointment);
        void DeleteAppointment(int? id);
        void SaveChanges();
    }
}
