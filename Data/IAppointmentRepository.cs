using CliniCareApp.Models;

namespace CliniCareApp.Data
{
    public interface IAppointmentRepository
    {
        void AddAppointment(Appointment appointment);
        List<Appointment> GetAllAppointments();
        Patient? GetPatientById(int? patientId);
        Patient? GetPatientByDni(string? patientDni);
        Appointment GetAppointmentById(int appointmentId);
        List<Appointment> GetAppointments(string patientDni);
        void UpdateAppointment(Appointment appointment);
        void DeleteAppointment(int? id);
        void SaveChanges();
    }
}
