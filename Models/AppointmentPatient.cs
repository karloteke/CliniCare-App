namespace CliniCareApp.Models
{
    public class AppointmentPatient
    {
        public int Id { get; set; }
        public DateTime Date { get; }
        public string? Area { get; set; }
        public string? Day { get; set; }
        public string? Time { get; set; }
        public bool IsUrgent { get; set; }
        public Patient? Patient { get; set; }


        // public AppointmentPatient() { }


        private static int NextId = 1;    

        public AppointmentPatient(DateTime date, string area, string day, string time, bool isUrgent, Patient patient)
        {
            Id = NextId++;
            Date = date;
            Area = area;
            Day = day;
            Time = time;
            IsUrgent = isUrgent;
            Patient = patient;
        }
    }
}




