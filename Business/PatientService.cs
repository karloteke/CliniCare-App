using ClinicApp.Models;
using System;

namespace ClinicApp.Business
{
    public class PatientService : IPatientService
    {
        public void CreatePatient()
        {
            Console.WriteLine("Nombre");
            string? name = Console.ReadLine();
            Console.WriteLine("");

            Console.WriteLine("Apellido");
            string? lastname = Console.ReadLine();
            Console.WriteLine("");

            Console.WriteLine("Dirección");
            string? address = Console.ReadLine();
            Console.WriteLine("");

            Console.WriteLine("DNI");
            string? dni = Console.ReadLine();
            Console.WriteLine("");

            Console.WriteLine("Teléfono");
            string? phone = Console.ReadLine();
            Console.WriteLine("");

            var newPatient = new Patient(name, lastname, address, dni, phone);
            Patient.AddPatient(newPatient);

            Console.WriteLine($"PACIENTE REGISTRADO CORRECTAMENTE");
        }

        public void ViewPatients()
        {
            var patients = Patient.GetPatients();

            if (patients.Count == 0)
            {
                Console.WriteLine("NO EXISTE NINGÚN PACIENTE EN LA LISTA");
            }
            else
            {
                foreach (var patient in patients)
                {
                    Console.WriteLine("---DATOS PACIENTE---");
                    Console.WriteLine("");
                    Console.WriteLine($"Id: {patient.Id}");
                    Console.WriteLine($"Nombre: {patient.Name}");
                    Console.WriteLine($"Apellido: {patient.LastName}");
                    Console.WriteLine($"Dirección: {patient.Address}");
                    Console.WriteLine($"Dni: {patient.Dni}");
                    Console.WriteLine($"Teléfono: {patient.Phone}");
                    Console.WriteLine("");
                }
            }
        }

        public void SearchByDni()
        {
            Console.WriteLine("Ingrese el DNI del paciente a buscar:");
            string? dniToSearch = Console.ReadLine();
            Console.WriteLine("");

            Patient? foundPatient = Patient.GetPatientByDni(dniToSearch);
            if (foundPatient != null)
            {
                Console.WriteLine("--- DATOS DE PACIENTE ENCONTRADO ---");
                Console.WriteLine("");
                Console.WriteLine($"Id: {foundPatient.Id}");
                Console.WriteLine($"Nombre: {foundPatient.Name}");
                Console.WriteLine($"Apellido: {foundPatient.LastName}");
                Console.WriteLine($"Dirección: {foundPatient.Address}");
                Console.WriteLine($"Dni: {foundPatient.Dni}");
                Console.WriteLine($"Teléfono: {foundPatient.Phone}");
            }
            else
            {
                Console.WriteLine("Paciente no encontrado.");
            }
        }
    }
}