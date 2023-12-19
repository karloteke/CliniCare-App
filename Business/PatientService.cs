using CliniCareApp.Models;
using CliniCareApp.Data;


namespace CliniCareApp.Business
{
    public class PatientService : IPatientService
    {
        private readonly IPatientRepository _repository;

        public PatientService(IPatientRepository repository)
        {
            _repository = repository;
        }
        public void CreatePatient()
        {
            Console.WriteLine("Nombre");
            string? name = Console.ReadLine();
            Console.WriteLine("");

            if(string.IsNullOrEmpty(name))
            {
                Console.WriteLine("Entrada inválida. El nombre no puede estar vacío.");
                return;
            }

            Console.WriteLine("Apellido");
            string? lastName = Console.ReadLine();
            Console.WriteLine("");

            if(string.IsNullOrEmpty(lastName))
            {
                Console.WriteLine("Entrada inválida. El apeliido no puede estar vacío.");
                return;
            }

            Console.WriteLine("Dirección");
            string? address = Console.ReadLine();
            Console.WriteLine("");

            if(string.IsNullOrEmpty(address))
            {
                Console.WriteLine("Entrada inválida. La dirección no puede estar vacía.");
                return;
            }

            Console.WriteLine("Número de DNI con letra");
            string? dni = Console.ReadLine();
            Console.WriteLine("");
            if (dni?.Length == 9)
            {
           
            }
            else
            {
                Console.WriteLine("DNI inválido. Tiene que tener 9 dígitos.");
                return;
            }

            var existPatient = _repository.GetPatientByDni(dni);
            
            if(existPatient != null)
            {
                Console.WriteLine("YA EXISTE ESE DNI");
                return; 
            }

            Console.WriteLine("Teléfono");
            string? phone = Console.ReadLine();
            Console.WriteLine("");

            if(string.IsNullOrEmpty(phone))
            {
                Console.WriteLine("Entrada inválida. El teléfono no puede estar vacío.");
                return;
            }


            var newPatient = new Patient(name, lastName, address, dni, phone);
     
                _repository.AddPatient(newPatient);
                _repository.UpdatePatient(newPatient);
                _repository.SaveChanges();

                Console.WriteLine("PACIENTE REGISTRADO CORRECTAMENTE");
        }      
    

        public void ViewPatients()
        {
            var patients = _repository.GetPatients();

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

            if(dniToSearch != null)
            {          
                Patient? foundPatient = _repository.GetPatientByDni(dniToSearch);

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
            else
            {
                Console.WriteLine("Entrada inválida. El DNI no puede estar vacío.");
            }
        }
    }
}