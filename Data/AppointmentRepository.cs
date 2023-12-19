using System.Text.Json;
using CliniCareApp.Models;

namespace CliniCareApp.Data
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private List<Appointment> _appointments = new List<Appointment>();
        private List<Patient> _patients = new List<Patient>();
        private readonly string _filePath = "appointments.json";
        private readonly IPatientRepository _patientRepository;

        public AppointmentRepository(IPatientRepository patientRepository)
        {
            _patientRepository = patientRepository;
            LoadAppointments();
        }

        public void AddAppointment(Appointment appointment)
        {
            _appointments.Add(appointment);
        }

        public List<Appointment> GetAppointments()
        {
            return _appointments;
        }

        public Patient? GetPatientById(int? patientId)
        {
            return _patientRepository.GetPatientById(patientId);
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
        }
    }
}
