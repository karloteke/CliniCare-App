using ClinicApp.Models;
using ClinicApp.Business;
using ClinicApp.Data;
using Spectre.Console;

class Menu
{
    private static string? choice = "";

    // Instancia pacientes
    private static IPatientRepository patientRepository = new PatientRepository();
    private static IPatientService patientService = new PatientService(patientRepository);

    // Instancia citas
    private static IAppointmentRepository appointmentRepository = new AppointmentRepository(patientRepository);
    private static IAppointmentService appointmentService = new AppointmentService(appointmentRepository); 

    // Instancia historial médico
    private static IMedicalRecordRepository medicalRecordRepository = new MedicalRecordRepository(patientRepository); 
    private static IMedicalRecordService medicalRecordService = new MedicalRecordService(medicalRecordRepository);

   // Instancia citas creadas por pacientes
    private static IAppointmentPatientRepository appointmentPatientRepository = new AppointmentPatientRepository(patientRepository);
    private static IAppointmentPatientService appointmentPatientService = new AppointmentPatientService(appointmentPatientRepository); 

    // Instacia Usuarios
    private static IUserRepository userRepository = new UserRepository();
    private static IUserService userService = new UserService(userRepository);

    // Instancia acceso a la zona privada
    private static PrivateAreaAccess privateAreaAccess = new PrivateAreaAccess(userService);

    
    public static void Main()
    {
        bool privateZone = false;
        
        do
        {
            if(privateZone)
            {
                Console.WriteLine(" === ZONA PRIVADA ===");
                Console.WriteLine(" ");
                Console.WriteLine("1. Insertar Pacientes");
                Console.WriteLine("   1.1 Visualizar pacientes");
                Console.WriteLine("2. Insertar citas médicas");
                Console.WriteLine("   2.1 Visualizar citas médicas");
                Console.WriteLine("3. Insertar historial médico");
                Console.WriteLine("   3.1 Visualizar historial médico");
                Console.WriteLine("4. Busqueda de paciente por Dni");
                Console.WriteLine("5. Ir a zona pública");
                Console.WriteLine("e. Salir");
                Console.WriteLine(" ");
            }
            else
            {
                Console.WriteLine("=== ZONA PUBLICA ===");
                Console.WriteLine(" ");
                Console.WriteLine("1. Pedir cita");
                Console.WriteLine("2. Visualizar citas");
                Console.WriteLine("3. Registro para personal médico");
                Console.WriteLine("4. Zona privada para personal médico ");  
                Console.WriteLine("e. Salir");
                Console.WriteLine(" ");
            }
      
            choice = Console.ReadLine();

            Console.WriteLine("");

            
            if (choice == "e")
            {
                Console.WriteLine("¡Hasta la próxima!");
                break; 
            }
      
            if(privateZone){
                
                switch (choice)
                {
                    case "1":
                        patientService.CreatePatient();
                        break;

                    case "1.1":
                        patientService.ViewPatients();
                        break;

                    case "2":
                        appointmentService.CreateAppointment();
                        break;   

                    case "2.1":
                        appointmentService.ViewAppointment();
                        break;

                    case "3":
                        medicalRecordService.CreateMedicalRecord();
                        break;   

                    case "3.1":
                        medicalRecordService.ViewMedicalRecord();
                        break;

                    case "4":
                        patientService.SearchByDni();
                        break;

                    case "5":
                        privateZone = false;
                        break;

                    default:
                        Console.WriteLine("OPCIÓN NO VÁLIDA");
                        break;
                }
            }
            else 
            {
                switch (choice)
                {
                    case "1":
                        appointmentPatientService.CreateAppointmentPatient();
                        break;
                    
                    case "2":
                        appointmentPatientService.ViewAppointmentPatient();
                        break;

                    case "3":
                        userService.CreateUser();
                        break;

                    case "4":
                        Console.WriteLine("Introduzca usuario y contraseña para entrar en la zona privada como personal médico");
                        Console.WriteLine(" ");

                        Console.Write("Usuario: ");
                        string? userName = Console.ReadLine();

                        Console.Write("Contraseña: ");
                        string? password = Console.ReadLine();
                        Console.WriteLine("");


                        if (userName != null && password != null)
                        {
                            bool authenticated = privateAreaAccess.Authentication(userName, password);

                            if (authenticated)
                            {
                                privateZone = true;
                                Console.WriteLine("BIENVENIDO A LA ZONA PRIVADA DE CLINICARE");
                            }
                            else
                            {
                                Console.WriteLine("ACCESO DENEGADO");
                            }
                        }
                        break;

                    default:
                        Console.WriteLine("OPCIÓN NO VALIDA");
                        break;
                }
            }
            Console.WriteLine("");
            Console.WriteLine(">>Presione enter para continuar");
            Console.ReadLine();
            
        } while(choice != "e");    
    }
}
