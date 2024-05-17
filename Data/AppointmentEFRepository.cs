using System.Globalization;
using CliniCareApp.Models;
using Microsoft.EntityFrameworkCore;

namespace CliniCareApp.Data
{
    public class AppointmentEFRepository : IAppointmentRepository
    {
        private readonly CliniCareContext _context;

        public AppointmentEFRepository(CliniCareContext context)
        {
            _context = context;
        }

        public void AddAppointment(Appointment appointment)
        {
            _context.Appointments.Add(appointment);
        }
        
        public IEnumerable<Appointment> GetAllAppointments(AppointmentQueryParameters? appointmentQueryParameters, bool orderByUrgentAsc) {
        var query = _context.Appointments.AsQueryable();

        
            if (!string.IsNullOrWhiteSpace(appointmentQueryParameters.Area)) {
                query = query.Where(a => a.Area.Contains(appointmentQueryParameters.Area));
            }

            if (!string.IsNullOrWhiteSpace(appointmentQueryParameters.MedicalName)) {
                query = query.Where(a => a.MedicalName.Contains(appointmentQueryParameters.MedicalName));
            }  

            if (orderByUrgentAsc) 
            {
                query = query.OrderByDescending(a => a.IsUrgent);
            }

            var result = query.ToList();
            return result;
        }

         public IEnumerable<Appointment> GetAppointmentsForPatient(AppointmentPatientQueryParameters? appointmentPatientQueryParameters, bool orderByDateAsc) {
            var query = _context.Appointments.AsQueryable();

        
            if (!string.IsNullOrWhiteSpace(appointmentPatientQueryParameters.PatientDni)) 
            {
                query = query.Where(p => p.PatientDni.Contains(appointmentPatientQueryParameters.PatientDni));
            }

            if (orderByDateAsc) {
                var appointments = query.ToList(); // Obtener todas las citas de la base de datos
                query = appointments.OrderBy(a => {
                    // Convertir la fecha 
                    if (DateTime.TryParseExact(a.Date, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parsedDate)) 
                    {
                        return parsedDate;
                    } 
                    else 
                    {        
                        return DateTime.MinValue;  // En caso de que la fecha no pueda ser convertida, devuelve un valor mínimo
                    }
                }).AsQueryable(); // Convertir la lista ordenada de nuevo a IQueryable
            }
  
            var result = query.ToList();
            return result;
        }

        
        public List<Appointment> GetAllAppointments()
        {
            return _context.Appointments.ToList();
        }

        public Patient? GetPatientById(int? patientId)
        {
            return _context.Patients.FirstOrDefault(p => p.Id == patientId);
        }

        public Patient? GetPatientByDni(string? patientDni)
        {
            return _context.Patients.FirstOrDefault(p => p.Dni == patientDni);
        }

        public Appointment GetAppointmentById(int appointmentId)
        {
            return _context.Appointments.FirstOrDefault(a => a.Id == appointmentId);
        }


        public List<Appointment> GetAppointments(string patientDni)
        {
            return _context.Appointments.ToList();
        }

        public void UpdateAppointment(Appointment appointment)
        {
            _context.Entry(appointment).State = EntityState.Modified;
        }

        public void DeleteAppointment(int? appointmentId)
        {
            var appointment = _context.Appointments.FirstOrDefault(a => a.Id == appointmentId);
            if (appointment != null)
            {
                _context.Appointments.Remove(appointment);
                _context.SaveChanges();
            }
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
