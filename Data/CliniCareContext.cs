using Microsoft.EntityFrameworkCore;
using CliniCareApp.Models;
using Microsoft.Extensions.Logging;

namespace CliniCareApp.Data
{
    public class CliniCareContext : DbContext
    {

        public CliniCareContext(DbContextOptions<CliniCareContext> options)
            : base(options)
        {

        }

    //   protected override void OnModelCreating(ModelBuilder modelBuilder) {
    //         modelBuilder.Entity<Patient>().HasData(
    //             new Patient { Name = "Carlota", LastName = "Cetina", Address="Dato", Dni="73000461W", Phone="654465115"},
    //             new Patient { Name = "Jesus", LastName = "Gimenez", Address="Olvido", Dni="73000461J", Phone="654465115"}
    //         );
    
        

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .LogTo(Console.WriteLine, LogLevel.Information)
                .EnableSensitiveDataLogging(); // Opcional
        }


        public DbSet<Patient> Patients { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        // public DbSet<MedicalRecord> MedicalRecords { get; set; }
        // public DbSet<AppointmentPatient> AppointmentPatients { get; set; }
        // public DbSet<User> Users { get; set; }
       
    }
}

