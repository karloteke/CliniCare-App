using System.Text.Json;
using CliniCareApp.Models;

namespace CliniCareApp.Data
{
    public class AppointmentPatientRepository : IAppointmentPatientRepository
    {
        private List<AppointmentPatient> _appointmentPatients = new List<AppointmentPatient>();
        private List<Patient> _patients;
        private readonly string _filePath;
         private readonly IPatientRepository _patientRepository;

        public AppointmentPatientRepository(IPatientRepository patientRepository)
        {
            _patientRepository = patientRepository; 
            _filePath = GetFilePath();
            LoadAppointments();
        }

        private string GetFilePath()
        {
            string basePath = Environment.GetEnvironmentVariable("IS_DOCKER") == "true" ? "SharedFolder" : "";
            return Path.Combine(basePath, "appointmentPatients.json");
        }

        public void AddAppointmentPatient(AppointmentPatient appointmentPatient)
        {
            //Verifico si existe la cita en la lista antes de agregarla
            if(!_appointmentPatients.Any(ap => ap.Id == appointmentPatient.Id) )
            {
                _appointmentPatients.Add(appointmentPatient);
                SaveChanges();
            }
        }


        // public List<AppointmentPatient> GetAppointmentPatients()
        // {
        //     return _appointmentPatients;
        // }
        public List<AppointmentPatient> GetAppointmentPatientsByDNI(string dni)
        {
            return _appointmentPatients
                .Where(ap => ap.Patient?.Dni == dni)
                .ToList();
        }

          public Patient? GetPatientByDni(string? dni)
        {
            return _patients.FirstOrDefault(p => p.Dni == dni);
        }
        
        public List<Patient> GetPatients()
        {
            return _patients;
        }

        // public List<AppointmentPatient> GetAllAppointmentPatients()
        // {
        //     return _appointmentPatients;
        // }

        public Patient? GetPatientById(int? patientId)
        {
            return _patientRepository.GetPatientById(patientId);
        }

        public AppointmentPatient GetAppointmentPatientById(int appointmentPatientId)
        {
            return _appointmentPatients.FirstOrDefault(ap => ap.Id == appointmentPatientId);
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
