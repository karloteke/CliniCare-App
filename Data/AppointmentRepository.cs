using System.Globalization;
using System.Text.Json;
using CliniCareApp.Models;

namespace CliniCareApp.Data
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private List<Appointment> _appointments = new List<Appointment>();
        private List<Patient> _patients = new List<Patient>();
        private readonly string _filePath;
        private readonly IPatientRepository _patientRepository;

        public AppointmentRepository(IPatientRepository patientRepository)
        {
            _patientRepository = patientRepository;
            _filePath = GetFilePath();
            LoadAppointments();
        }

        private string GetFilePath()
        {
            string basePath = Environment.GetEnvironmentVariable("IS_DOCKER") == "true" ? "SharedFolder" : "";
            return Path.Combine(basePath, "appointments.json");
        }

        public void AddAppointment(Appointment appointment)
        {
            //Verifico si existe la cita en la lista antes de agregarla
            // if(!_appointments.Any(a => a.Id == appointment.Id) )
            // {
                _appointments.Add(appointment);
                SaveChanges();
            // }
        }

        
        public IEnumerable<Appointment> GetAllAppointments(AppointmentQueryParameters? appointmentQueryParameters, bool orderByUrgentAsc)
        {
            var query = _appointments.AsQueryable();


            if (!string.IsNullOrWhiteSpace(appointmentQueryParameters.Area)) 
            {
            query = query.Where(a => a.Area.Contains(appointmentQueryParameters.Area));
            }

            if (!string.IsNullOrWhiteSpace(appointmentQueryParameters.MedicalName)) 
            {
            query = query.Where(a => a.MedicalName.Contains(appointmentQueryParameters.MedicalName));
            }

            if (orderByUrgentAsc) 
            {
                query = query.OrderByDescending(a => a.IsUrgent);
            }
            

            var result = query.ToList();
            return result;
        }

        public IEnumerable<Appointment> GetAppointmentsForPatient(AppointmentPatientQueryParameters? appointmentPatientQueryParameters, bool orderByDateAsc)
        {
            var query = _appointments.AsQueryable();


            if (!string.IsNullOrWhiteSpace(appointmentPatientQueryParameters.PatientDni)) 
            {
            query = query.Where(p => p.PatientDni.Contains(appointmentPatientQueryParameters.PatientDni));
            }

            if (orderByDateAsc)
            {
                 query = query.OrderBy(a => a.Date);
            }
         
            var result = query.ToList();
            return result;
        }



        public List<Appointment> GetAllAppointments()
        {
            return _appointments;
        }

        public Patient? GetPatientById(int? patientId)
        {
            return _patientRepository.GetPatientById(patientId);
        }

        public Patient? GetPatientByDni(string? patientDni)
        {
            return _patientRepository.GetPatientByDni(patientDni);
        }

        public Appointment GetAppointmentById(int appointmentId)
        {
            return _appointments.FirstOrDefault(a => a.Id == appointmentId);
        }

        public List<Appointment> GetAppointments(string patientDni)
        {
            // Implementación para obtener todas las citas asociadas a un paciente por su DNI
            return _appointments.Where(ap => ap.PatientDni == patientDni).ToList();
        }

        public void DeleteAppointment(int? appointmentId)
        {
            if (appointmentId != null)
            {
                var appointment = _appointments.FirstOrDefault(a => a.Id == appointmentId);
                if (appointment != null)
                {
                    _appointments.Remove(appointment);
                    SaveChanges();
                }
            }
        }

        public void UpdateAppointment(Appointment appointment)
        {
            AddAppointment(appointment);
        }

        public void SaveChanges()
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            string jsonString = JsonSerializer.Serialize(_appointments, options);
            File.WriteAllText(_filePath, jsonString);
        }

        private void LoadAppointments()
        {
            if (File.Exists(_filePath))
            {
                string jsonString = File.ReadAllText(_filePath);
                var appointments = JsonSerializer.Deserialize<List<Appointment>>(jsonString);
                _appointments = appointments ?? new List<Appointment>();
            }

            if (_appointments.Any())
            {
                // int maxId = _appointments.Max(a => a.Id);
                // Appointment.UpdateNextAppointmentId(maxId + 1);
            }
        }
    }
}
