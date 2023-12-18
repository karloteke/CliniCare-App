using System.Text.Json;
using ClinicApp.Models;

namespace ClinicApp.Data
{
    public class AppointmentPatientRepository : IAppointmentPatientRepository
    {
        private List<AppointmentPatient> _appointmentPatients = new List<AppointmentPatient>();
        private readonly string _filePath = "appointmentPatients.json";  
        private readonly IPatientRepository _patientRepository;

        public AppointmentPatientRepository(IPatientRepository patientRepository)
        {
            _patientRepository = patientRepository;
            LoadAppointments();
        }

        public void AddAppointmentPatient(AppointmentPatient appointmentPatient)
        {
            _appointmentPatients.Add(appointmentPatient);
        }

        public List<AppointmentPatient> GetAppointmentPatients()
        {
            return _appointmentPatients;
        }
        public List<AppointmentPatient> GetAppointmentPatientsByDNI(string dni)
        {
            return _appointmentPatients
                .Where(ap => ap.Patient?.Dni == dni)
                .ToList();
        }

        public void UpdateAppointmentPatient(AppointmentPatient appointmentPatient)
        {
            AddAppointmentPatient(appointmentPatient);
        }

        public void SaveChanges()
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            string jsonString = JsonSerializer.Serialize(_appointmentPatients, options);
            File.WriteAllText(_filePath, jsonString);
        }

        private void LoadAppointments()
        {
            if (File.Exists(_filePath))
            {
                string jsonString = File.ReadAllText(_filePath);
                var appointmentPatients = JsonSerializer.Deserialize<List<AppointmentPatient>>(jsonString);
                _appointmentPatients = appointmentPatients ?? new List<AppointmentPatient>();
                
            }
        }
    }
}
