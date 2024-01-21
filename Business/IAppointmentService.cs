using System.Collections.Generic; 
using CliniCareApp.Models; 

namespace CliniCareApp.Business
{
    public interface IAppointmentService
    {
        void CreateAppointment(int patientId, DateTime appointmentDate, string area, string medicalName, string date, string time, bool isUrgent);  
        List<Appointment> GetAppointments();
    }
}