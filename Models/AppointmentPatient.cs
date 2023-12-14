namespace ClinicApp.Models
{
    public class AppointmentPatient
    {
        public int Id { get; private set; }
        public DateTime Date { get; }
        public string? Area { get; set; }
        public string? Day { get; set; }
        public string? Time { get; set; }
        public bool IsUrgent { get; set; }
        public Patient? Patient { get; set; }

        private static int NextId = 1;
        private static readonly List<AppointmentPatient> Appointments = new List<AppointmentPatient>();
        

        public AppointmentPatient(DateTime date, string area, string day, string time, bool isUrgent)
        {
            Id = NextId++;
            Date = date;
            Area = area;
            Day = day;
            Time = time;
            IsUrgent = isUrgent;
        }
    }
}




