using CliniCareApp.Models;
using CliniCareApp.Business;
using CliniCareApp.Data;
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
                AnsiConsole.WriteLine();
                 AnsiConsole.MarkupLine("[bold invert yellow1]BIENVENIDO A LA ZONA PRIVADA DE CliniCare[/]");
                AnsiConsole.WriteLine();
                var table = new Table().BorderColor(Color.Purple);
                table.AddColumn(new TableColumn("Opción").Centered().Width(20));
                table.AddColumn(new TableColumn("Descripción").Centered().Width(40)); 

                table.AddRow("1", "Insertar Pacientes");
                table.AddRow("1.1", "Visualizar pacientes");
                table.AddEmptyRow();
                table.AddRow("2", "Insertar citas médicas");
                table.AddRow("2.1", "Visualizar citas médicas");
                table.AddEmptyRow();
                table.AddRow("3", "Insertar historial médico");
                table.AddRow("3.1", "Visualizar historial médico");
                table.AddEmptyRow();
                table.AddRow("4", "Búsqueda de paciente por Dni");
                table.AddRow("5", "Ir a zona pública");
                table.AddRow("e", "Salir");

                AnsiConsole.Write(table);
            }
            else
            {
                AnsiConsole.WriteLine();
                AnsiConsole.MarkupLine("[bold invert yellow1]BIENVENIDO A LA ZONA PÚBLICA DE CliniCare[/]");
                AnsiConsole.WriteLine();
                var table = new Table().BorderColor(Color.Purple);
                table.AddColumn(new TableColumn("Opción").Centered().Width(20)); 
                table.AddColumn(new TableColumn("Descripción").Centered().Width(40)); 

                table.AddRow("1", "Pedir cita");
                table.AddRow("2", "Visualizar citas");
                table.AddRow("3", "Registro para personal médico");
                table.AddRow("4", "Zona privada para personal médico");
                table.AddRow("e", "Salir");

                AnsiConsole.Write(table);
            }
      
            choice = Console.ReadLine();

            Console.WriteLine("");

            
            if (string.Equals(choice, "e", StringComparison.OrdinalIgnoreCase))
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
                        Console.WriteLine("Nombre");
                        string? name = Console.ReadLine();
                        Console.WriteLine("");
                        if (string.IsNullOrEmpty(name))
                        {
                            Console.WriteLine("El nombre no puede estar vacío. Presione Enter para continuar.");
                            Console.ReadLine();
                            continue;
                        }

                        Console.WriteLine("Apellido");
                        string? lastName = Console.ReadLine();
                        Console.WriteLine("");
                        if (string.IsNullOrEmpty(lastName))
                        {
                            Console.WriteLine("El apellido no puede estar vacío. Presione Enter para continuar.");
                            Console.ReadLine();
                            continue;
                        }

                        Console.WriteLine("Dirección");
                        string? address = Console.ReadLine();
                        Console.WriteLine("");
                        if (string.IsNullOrEmpty(address))
                        {
                            Console.WriteLine("La dirección no puede estar vacía. Presione Enter para continuar.");
                            Console.ReadLine();
                            continue;
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
                            continue;
                        }

                        Console.WriteLine("Teléfono");
                        string? phone = Console.ReadLine();
                        Console.WriteLine("");
                        if (string.IsNullOrEmpty(phone))
                        {
                            Console.WriteLine("El nombre no puede estar vacío. Presione Enter para continuar.");
                            Console.ReadLine();
                            continue;
                        }

                            var newPatient = new Patient(name, lastName, address, dni, phone);
                            appointmentPatientService.CreateAppointmentPatient(newPatient);

                        break;
                    
                    case "2":
                        Console.WriteLine("Ingrese el DNI del paciente:");
                        string? patientDni = Console.ReadLine();

                        if (!string.IsNullOrEmpty(patientDni))
                        {
                            appointmentPatientService.ViewAppointmentPatient(patientDni);    
                        }
                        else
                        {
                            Console.WriteLine("EL NÚMERO DE DNI NO PUEDE ESTAR VACIO");
                        }
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

                        if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
                        {
                            Console.WriteLine("Usuario o contraseña no válidos. Ambos campos son obligatorios.");
                            continue;  
                        }

                    bool authenticated = privateAreaAccess.Authentication(userName, password);

                        if (authenticated)
                        {
                            privateZone = true;
                        }
                        else
                        {
                            Console.WriteLine("ACCESO DENEGADO");
                        }
                        break;

                    default:
                        Console.WriteLine("OPCIÓN NO VALIDA");
                        break;
                }
            }
            Console.WriteLine("");
            AnsiConsole.MarkupLine("[green]>> Presione enter para continuar[/]");
            Console.ReadLine();
            
        } while(choice != "e");    
    }
}
