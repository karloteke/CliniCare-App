namespace CliniCareApp.Models
{
    public class AppointmentPatient
    {
        public int Id { get; private set; }
        public DateTime Date { get; }
        public string? Area { get; set; }
        public string? Day { get; set; }
        public string? Time { get; set; }
        public bool IsUrgent { get; set; }

        // Información del paciente directamente en la cita
        public string? PatientName { get; set; }
        public string? PatientLastName { get; set; }
        public string? PatientAddress { get; set; }
        public string? PatientDni { get; set; }
        public string? PatientPhone { get; set; }

        private static int NextId = 1;    

        public AppointmentPatient(DateTime date, string area, string day, string time, bool isUrgent, string patientName, string patientLastName, string patientDni, string patientAddress, string patientPhone)
        {
            Id = NextId++;
            Date = date;
            Area = area;
            Day = day;
            Time = time;
            IsUrgent = isUrgent;
            PatientName = patientName;
            PatientLastName = patientLastName;
            PatientAddress = patientAddress;
            PatientDni = patientDni;
            PatientPhone = patientPhone;
        }
    }
}




