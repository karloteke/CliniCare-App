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

       protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Llamamos al método base para asegurarnos de que se ejecute cualquier lógica definida allí.
            base.OnModelCreating(modelBuilder);

            // Agregar pacientes
            modelBuilder.Entity<Patient>().HasData(
                new Patient { Id = 1, Name = "Carlota", LastName = "Cetina", Address = "C/Dato,23", Dni = "73000461W", Phone = "654465114" },
                new Patient { Id = 2, Name = "Jesus", LastName = "Gimenez", Address = "C/Olvido,33", Dni = "12345678J", Phone = "654465113" }
            );

            // Agregar usuarios User/Admin
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    UserName = "Carlota",
                    Password = "Carlota36", 
                    Email = "carlota@clinicare.com",
                    Role = Roles.Admin
                },
                new User
                {
                    Id = 2,
                    UserName = "Jesus",
                    Password = "Jesus30", 
                    Email = "jesus@clinicare.com",
                    Role = Roles.Admin
                },
                new User
                {
                    Id = 3,
                    UserName = "Paola",
                    Password = "Paola30", 
                    Email = "paola@gmail.com",
                    Role = Roles.User
                },
                new User
                {
                    Id = 4,
                    UserName = "Nerea",
                    Password = "Nerea30", 
                    Email = "nerea@gmail.com",
                    Role = Roles.User
                }
            );
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .LogTo(Console.WriteLine, LogLevel.Information)
                .EnableSensitiveDataLogging(); // Opcional
        }

        public DbSet<Patient> Patients { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<MedicalRecord> MedicalRecords { get; set; }
        public DbSet<User> Users { get; set; }
       
    }
}

