using System.Collections.Generic; 
using CliniCareApp.Models; 

namespace CliniCareApp.Business
{
    public interface IAppointmentService
    {
        void CreateAppointment(int patientId, DateTime createdAt, string area, string medicalName, string date, string time, bool isUrgent);  
        List<Appointment> GetAllAppointments();
        Appointment GetAppointmentById(int appointmentId);
        public void UpdateAppointmentDetails(int appointmentId, AppointmentUpdateDTO appointmentUpdateDTO);
        public void DeleteAppointment(int appointmentId);
    }
}