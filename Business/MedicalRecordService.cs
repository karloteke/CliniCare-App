using ClinicApp.Models;
using ClinicApp.Data;
using System;

namespace ClinicApp.Business
{
    public class MedicalRecordService : IMedicalRecordService
    {
        private readonly IMedicalRecordRepository _repository;

        public MedicalRecordService(IMedicalRecordRepository repository)
        {
            _repository = repository;
        }

        public void CreateMedicalRecord()
        {
            DateTime date = DateTime.Now;
            Console.WriteLine($"Fecha y hora: {date}");

            Console.WriteLine("Nombre del médico:");
            string? doctorName = Console.ReadLine();

             if(string.IsNullOrEmpty(doctorName))
            {
                Console.WriteLine("Entrada inválida. El nombre del médico no puede estar vacío.");
                return;
            }

            Console.WriteLine("Tratamiento:");
            string? treatment = Console.ReadLine();

             if(string.IsNullOrEmpty(treatment))
            {
                Console.WriteLine("Entrada inválida. El campo no puede estar vacío.");
                return;
            }

            Console.WriteLine("Coste del tratamiento (Ingrese un número decimal):");
            string? treatmentCostInput = Console.ReadLine();

            if (decimal.TryParse(treatmentCostInput, out decimal treatmentCost)&& treatmentCost % 1 != 0)
            {        
                
            }
            else
            {
                Console.WriteLine("Valor no válido.Ingrese número decimal.");
                return;
            }

            Console.WriteLine("Notas:");
            string? notes = Console.ReadLine();

             if(string.IsNullOrEmpty(notes))
            {
                Console.WriteLine("Entrada inválida. El campo no puede estar vacío.");
                return;
            }

            Console.WriteLine("");
            Console.WriteLine("Introduce el ID del paciente al que quieres asignarle el informe médico");

            int PatientId;

            if (!int.TryParse(Console.ReadLine(), out PatientId))
            {
                Console.WriteLine("ID de paciente no válido. Debe ser un número entero.");
                return;
            }
            
            var patient = _repository.GetPatientById(PatientId);

            if (patient != null)
            {
                
                var newMedicalRecord = new MedicalRecord(date, doctorName, treatment, treatmentCost, notes)
                {
                    Patient = patient
                };

                _repository.AddMedicalRecord(newMedicalRecord);
                _repository.SaveChanges();

                Console.WriteLine("");
                Console.WriteLine($"HISTORIAL MÉDICO REGISTRADO CORRECTAMENTE PARA: {patient.Name} {patient.LastName}");
            }
            else
            {
                Console.WriteLine($"No se encontró al paciente con ID {PatientId}");
            }
        }

        public void ViewMedicalRecord()
        {
            var medicalRecords = _repository.GetMedicalRecords();

            if (medicalRecords.Count == 0)
            {
                Console.WriteLine("NO EXISTE NINGÚN HISTORIAL MÉDICO PARA MOSTRAR");
            }
            else
            {

                foreach (var medicalRecord in medicalRecords)
                {
                    Console.WriteLine("---DATOS HISTORIAL MÉDICO---");
                    Console.WriteLine("");
                    Console.WriteLine($"Id historial médico: {medicalRecord.Id}");
                    Console.WriteLine($"Paciente: {medicalRecord.Patient?.Name} {medicalRecord.Patient?.LastName} con id {medicalRecord.Patient?.Id}");
                    Console.WriteLine($"Fecha y hora: {medicalRecord.CreatedAt}");
                    Console.WriteLine($"Nombre médico: {medicalRecord.DoctorName}");
                    Console.WriteLine($"Tratamiento: {medicalRecord.Treatment}");
                    Console.WriteLine($"Coste del tratamiento: {medicalRecord.TreatmentCost}");
                    Console.WriteLine($"Notas: {medicalRecord.Notes}");
                    Console.WriteLine(""); 
                }
            }
        }
    }
}