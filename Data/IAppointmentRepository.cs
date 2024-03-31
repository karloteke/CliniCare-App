using CliniCareApp.Models;

namespace CliniCareApp.Data
{
    public interface IAppointmentRepository
    {
        void AddAppointment(Appointment appointment);
        List<Appointment> GetAllAppointments();
        public IEnumerable<Appointment> GetAllAppointments(AppointmentQueryParameters? appointmentQueryParameters, bool orderByUrgentAsc);
        Patient? GetPatientById(int? patientId);
        Patient? GetPatientByDni(string? patientDni);
        Appointment GetAppointmentById(int appointmentId);
        List<Appointment> GetAppointments(string patientDni);
        void UpdateAppointment(Appointment appointment);
        void DeleteAppointment(int? id);
        void SaveChanges();
    }
}
