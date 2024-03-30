using System;
using System.Collections.Generic;
using System.Linq;
using CliniCareApp.Models;
using Microsoft.EntityFrameworkCore;

namespace CliniCareApp.Data
{
    public class PatientEFRepository : IPatientRepository
    {
        private readonly CliniCareContext _context;

        public PatientEFRepository(CliniCareContext context)
        {
            _context = context;
        }

        public void AddPatient(Patient patient)
        {
            _context.Patients.Add(patient);
        }

        
        public IEnumerable<Patient> GetAllPatients(PatientQueryParameters? patientQueryParameters, bool orderByNameAsc) {
        var query = _context.Patients.AsQueryable();

    
            if (!string.IsNullOrWhiteSpace(patientQueryParameters.Dni)) {
                query = query.Where(p => p.Dni.Contains(patientQueryParameters.Dni));
            }

            if (!string.IsNullOrWhiteSpace(patientQueryParameters.Name)) {
                query = query.Where(p => p.Name.Contains(patientQueryParameters.Name));
            }

            if (!string.IsNullOrWhiteSpace(patientQueryParameters.LastName)) {
                query = query.Where(p => p.LastName.Contains(patientQueryParameters.LastName));
            }  

            if (orderByNameAsc) {
                query = query.OrderBy(p => p.Name);
            }

            var result = query.ToList();
            return result;
        }


        public List<Patient> GetPatients()
        {
            return _context.Patients.ToList();
        }

        public Patient? GetPatientByDni(string? dni)
        {
            return _context.Patients.FirstOrDefault(p => p.Dni == dni);
        }

        public Patient? GetPatientById(int? patientId)
        {
            return _context.Patients.FirstOrDefault(p => p.Id == patientId);
        }

        public void UpdatePatient(Patient patient)
        {
            // _context.Patients.Update(patient);
            // _context.SaveChanges();
              // En EF Core, si el objeto ya está siendo rastreado, actualizar sus propiedades
            // y llamar a SaveChanges() es suficiente para actualizarlo en la base de datos.
            // Asegúrate de que el estado del objeto sea 'Modified' si es necesario.
            _context.Entry(patient).State = EntityState.Modified;
        }

        public void DeletePatient(int? patientId)
        {
            var patient = _context.Patients.FirstOrDefault(p => p.Id == patientId);
            if (patient != null)
            {
                _context.Patients.Remove(patient);
                _context.SaveChanges();
            }
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
