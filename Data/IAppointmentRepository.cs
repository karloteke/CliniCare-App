using CliniCareApp.Models;

namespace CliniCareApp.Data
{
    public interface IAppointmentRepository
    {
        void AddAppointment(Appointment appointment);
        List<Appointment> GetAppointments();
        Appointment GetAppointmentById(int appointmentId);
        void UpdateAppointment(Appointment appointment);
        void DeleteAppointment(int? id);
        void SaveChanges();
    }
}
