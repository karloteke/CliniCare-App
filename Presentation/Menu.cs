using CliniCareApp.Models;
using CliniCareApp.Business;
using CliniCareApp.Data;
using Spectre.Console;

class Menu
{
    private static string? choice = "";
    
    public static void Run(IPatientService patientService,
        IAppointmentService appointmentService,
        IMedicalRecordService medicalRecordService,
        IAppointmentPatientService appointmentPatientService,
        IUserService userService,
        PrivateAreaAccess privateAreaAccess)
    {
        bool privateZone = false;
        int patientId = 0;
        DateTime medicalRecordDate;
        DateTime appointmentDate;
        
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
                AnsiConsole.MarkupLine("[blue]¡Hasta la próxima![/]");
                break; 
            }
      

            if(privateZone){

                switch (choice)
                {
                    case "1":
                        AnsiConsole.MarkupLine("[purple]Nombre[/]");
                        string? name = Console.ReadLine();
                        Console.WriteLine("");

                        if(string.IsNullOrEmpty(name))
                        {
                            AnsiConsole.MarkupLine("[red]Entrada inválida. El nombre no puede estar vacío[/]");
                            continue;
                        }

                        AnsiConsole.MarkupLine("[purple]Apellido[/]");
                        string? lastName = Console.ReadLine();
                        Console.WriteLine("");

                        if(string.IsNullOrEmpty(lastName))
                        {
                            AnsiConsole.MarkupLine("[red]Entrada inválida. El apeliido no puede estar vacío[/]");
                            continue;
                        }

                        AnsiConsole.MarkupLine("[purple]Dirección[/]");
                        string? address = Console.ReadLine();
                        Console.WriteLine("");

                        if(string.IsNullOrEmpty(address))
                        {
                            AnsiConsole.MarkupLine("[red]Entrada inválida. La dirección no puede estar vacía[/]");
                            continue;
                        }

                        AnsiConsole.MarkupLine("[purple]Número de DNI con letra[/]");
                        string? dni = Console.ReadLine();
                        Console.WriteLine("");
                        if (dni?.Length != 9)
                        {
                            AnsiConsole.MarkupLine("[red]DNI inválido. Tiene que tener 9 caracteres[/]");
                            continue;
                        }

                        var existPatient = patientService.GetPatientByDni(dni);
            
                        if(existPatient != null)
                        {
                            AnsiConsole.MarkupLine("[red]Ya existe ese DNI[/]");
                            continue; 
                        }

                        AnsiConsole.MarkupLine("[purple]Teléfono[/]");
                        string? phone = Console.ReadLine();
                        Console.WriteLine("");

                        if(string.IsNullOrEmpty(phone) || phone.Length < 9)
                        {
                            AnsiConsole.MarkupLine("[red]Entrada inválida. El teléfono es obligatorio y debe de tener al menos 9 caracteres[/]");
                            continue;
                        }
                    
                        patientService.CreatePatient(name, lastName, address, dni, phone);
                        AnsiConsole.MarkupLine("[green]PACIENTE REGISTRADO CORRECTAMENTE[/]");
                        break;

                    case "1.1":
                        var patients = patientService.ViewPatients();
                        foreach (var p in patients)
                        {
                            AnsiConsole.MarkupLine("[bold invert yellow1]DATOS PACIENTE[/]");
                            Console.WriteLine("");
                            Console.WriteLine($"Id: {p.Id}");
                            Console.WriteLine($"Nombre: {p.Name}");
                            Console.WriteLine($"Apellido: {p.LastName}");
                            Console.WriteLine($"Dirección: {p.Address}");
                            Console.WriteLine($"Dni: {p.Dni}");
                            Console.WriteLine($"Teléfono: {p.Phone}");
                            Console.WriteLine("");
                        }
                        break;

                    case "2":
                        appointmentDate = DateTime.Now;
                        AnsiConsole.MarkupLine($"[yellow1]Fecha y hora de creación: {appointmentDate}[/]");

                        AnsiConsole.MarkupLine("[purple]Especialidad (Oftalmología/traumatología/ginecología/neurología)[/]");
                        string? area = Console.ReadLine();
                        Console.WriteLine("");

                        if(string.IsNullOrEmpty(area))
                        {
                            AnsiConsole.MarkupLine("[red]Entrada inválida. La especialidad no puede estar vacía[/]");
                            continue;
                        }

                        AnsiConsole.MarkupLine("[purple]Nombre del médico[/]");
                        string? medicalName = Console.ReadLine();
                        Console.WriteLine("");

                        if(string.IsNullOrEmpty(medicalName))
                        {
                            AnsiConsole.MarkupLine("[red]Entrada inválida. El nombre del médico no puede estar vacío[/]");
                            continue;
                        }

                        AnsiConsole.MarkupLine("[purple]Fecha (dd/MM/yyyy)[/]");
                        string? date = Console.ReadLine();
                        Console.WriteLine("");

                        if(date == null)
                        {
                            AnsiConsole.MarkupLine("[red]Entrada inválida. La fecha no puede estar vacía[/]");
                            continue;
                        }

                        if (!FechaValida(date))
                        {
                            AnsiConsole.MarkupLine("[red]Fecha inválida. Debe tener el formato dd/MM/yyyy[/]");
                            continue;
                        }
                        
                        AnsiConsole.MarkupLine("[purple]Hora (HH:mm)[/]");
                        string? time = Console.ReadLine();
                        Console.WriteLine("");

                        if (time == null)
                        {
                            AnsiConsole.MarkupLine("[red]Entrada inválida. La hora no puede estar vacía[/]");
                            continue;
                        }

                        if (!HoraValida(time))
                        {
                            AnsiConsole.MarkupLine("[red]Hora inválida. Debe tener el formato HH:mm[/]");
                            continue;
                        }

                        bool isUrgent;

                        AnsiConsole.MarkupLine("[purple]¿Es urgente? (si/no)[/]");
                        string? isUrgentInput = Console.ReadLine();
                
                        if(isUrgentInput == "no")
                        {
                            isUrgent = false;
                        }
                        else if(isUrgentInput == "si")
                        {
                            isUrgent = true;
                        }
                        else
                        {
                            AnsiConsole.MarkupLine("[red]Respuesta incorrecta. Escriba si o no[/]");
                            continue;
                        }
                        
                        Console.WriteLine ("");
                        AnsiConsole.MarkupLine ("[purple]Introduce el ID del paciente al que quieres asignarle la cita[/]");
                        string? patientIdInput = Console.ReadLine();

                        if (!int.TryParse(patientIdInput, out patientId)) 
                        {
                            AnsiConsole.MarkupLine("[red]ID de paciente inválido. Debe ser un número entero[/]");
                            continue;
                        }

                        var patient = patientService.GetPatientById(patientId);
                        if (patient != null)
                        {
                            appointmentService.CreateAppointment(patientId, appointmentDate,area, medicalName, date, time, isUrgent);
                            Console.WriteLine("");
                           AnsiConsole.MarkupLine($"[green]CITA REGISTRADA CORRECTAMENTE PARA: {patient?.Name} {patient?.LastName}[/]");
                        }
                        else
                        {
                        AnsiConsole.MarkupLine("[red]No se encontró un paciente con el ID proporcionado[/]");
                        }
                        break; 

                    case "2.1":
                        var appointments = appointmentService.GetAppointments();

                        foreach (var appointment in appointments)
                        {
                            AnsiConsole.MarkupLine("[bold invert yellow1]DATOS CITA[/]");
                            Console.WriteLine("");
                            Console.WriteLine($"Fecha y hora de registro: {appointment.CreatedAt}"); 
                            Console.WriteLine($"Id cita: {appointment.Id}");
                            Console.WriteLine($"Paciente: {appointment.Patient?.Name} {appointment.Patient?.LastName}");
                            Console.WriteLine($"Especialidad: {appointment.Area}");
                            Console.WriteLine($"Nombre médico: {appointment.MedicalName}");
                            Console.WriteLine($"Hora: {appointment.Time}");
                            Console.WriteLine($"Día: {appointment.Date}");
                            Console.WriteLine($"¿Es urgente?: {(appointment.IsUrgent ? "si" : "no")}");
                            Console.WriteLine(""); 
                        }    
                        break;

                    case "3":
                        medicalRecordDate = DateTime.Now;
                        AnsiConsole.MarkupLine($"[yellow1]Fecha y hora de creación: {medicalRecordDate}[/]");

                        AnsiConsole.MarkupLine("[purple]Nombre del médico:[/]");
                        string? doctorName = Console.ReadLine();

                        if(string.IsNullOrEmpty(doctorName))
                        {
                            AnsiConsole.MarkupLine("[red]Entrada inválida. El nombre del médico no puede estar vacío[/]");
                            continue;
                        }

                        AnsiConsole.MarkupLine("[purple]Tratamiento:[/]");
                        string? treatment = Console.ReadLine();

                        if(string.IsNullOrEmpty(treatment))
                        {
                            AnsiConsole.MarkupLine("[red]Entrada inválida. El campo no puede estar vacío[/]");
                            continue;
                        }

                        AnsiConsole.MarkupLine("[purple]Coste del tratamiento (Ingrese un número decimal):[/]");
                        string? treatmentCostInput = Console.ReadLine();

                        if (decimal.TryParse(treatmentCostInput, out decimal treatmentCost)&& treatmentCost % 1 != 0)
                        {        
                            
                        }
                        else
                        {
                            AnsiConsole.MarkupLine("[red]Valor no válido.Ingrese un número decimal[/]");
                            continue;
                        }

                        AnsiConsole.MarkupLine("[purple]Notas:[/]");
                        string? notes = Console.ReadLine();

                        if(string.IsNullOrEmpty(notes))
                        {
                            AnsiConsole.MarkupLine("[red]Entrada inválida. El campo no puede estar vacío[/]");
                            continue;
                        }

                        Console.WriteLine("");
                        AnsiConsole.MarkupLine("[purple]Introduce el ID del paciente al que quieres asignarle el informe médico[/]");
                        string? patientIdInputMedicalRecord = Console.ReadLine();

                        if (!int.TryParse(patientIdInputMedicalRecord, out patientId)) 
                        {
                            AnsiConsole.MarkupLine("[red]ID de paciente inválido. Debe ser un número entero[/]");
                            continue;
                        }

                        var patientForMedicalRecord = patientService.GetPatientById(patientId);

                        if (patientForMedicalRecord != null)
                        {
                            medicalRecordService.CreateMedicalRecord(patientId, medicalRecordDate, doctorName, treatment, treatmentCost, notes);
                            AnsiConsole.MarkupLine($"[green]Historial médico registrado correctamente para: {patientForMedicalRecord.Name} {patientForMedicalRecord.LastName}[/]");
                        }
                        else
                        {
                            AnsiConsole.MarkupLine("[red]No se encontró un paciente con el ID proporcionado[/]");
                        }
                        break;

                    case "3.1":
                        var medicalRecords = medicalRecordService.GetMedicalRecords();

                        foreach (var medicalRecord in medicalRecords)
                        {
                            AnsiConsole.MarkupLine("[bold invert yellow1]DATOS HISTORIAL MÉDICO[/]");
                            Console.WriteLine("");
                            Console.WriteLine($"Fecha y hora de registro: {medicalRecord.CreatedAt}");
                            Console.WriteLine($"Id historial médico: {medicalRecord.Id}");
                            Console.WriteLine($"Paciente: {medicalRecord.Patient?.Name} {medicalRecord.Patient?.LastName}");
                            Console.WriteLine($"Nombre médico: {medicalRecord.DoctorName}");
                            Console.WriteLine($"Tratamiento: {medicalRecord.Treatment}");
                            Console.WriteLine($"Coste del tratamiento: {medicalRecord.TreatmentCost}");
                            Console.WriteLine($"Notas: {medicalRecord.Notes}");
                            Console.WriteLine(""); 
                        }
                        break;

                    case "4":
                        AnsiConsole.MarkupLine("[purple]Ingrese el DNI del paciente a buscar:[/]");
                        string? dniToSearch = Console.ReadLine();
                        Console.WriteLine("");

                        if (!string.IsNullOrEmpty(dniToSearch))
                        {
                            // Llamar al método de búsqueda del servicio
                            Patient? foundPatient = patientService.SearchByDni(dniToSearch);

                            if (foundPatient != null)
                            {
                                AnsiConsole.MarkupLine("[bold invert yellow1]DATOS DE PACIENTE ENCONTRADO[/]");
                                Console.WriteLine($"Id: {foundPatient.Id}");
                                Console.WriteLine($"Nombre: {foundPatient.Name}");
                                Console.WriteLine($"Apellido: {foundPatient.LastName}");
                                Console.WriteLine($"Dirección: {foundPatient.Address}");
                                Console.WriteLine($"Dni: {foundPatient.Dni}");
                                Console.WriteLine($"Teléfono: {foundPatient.Phone}");
                            }
                            else
                            {
                                AnsiConsole.MarkupLine("[red]Paciente no encontrado[/]");
                            }
                        }
                        else
                        {
                            AnsiConsole.MarkupLine("[red]Entrada inválida. El DNI no puede estar vacío[/]");
                        }
                        break;

                    case "5":
                        privateZone = false;
                        break;

                    default:
                        AnsiConsole.MarkupLine("[red]OPCIÓN NO VÁLIDA[/]");
                        break;
                }
            }
            else 
            {
                switch (choice)
                {
                    case "1":

                        AnsiConsole.MarkupLine("[purple]Nombre[/]");
                        string? name = Console.ReadLine();
                        Console.WriteLine("");
                        if (string.IsNullOrEmpty(name))
                        {
                            AnsiConsole.MarkupLine("[red]El nombre no puede estar vacío[/]");
                            Console.ReadLine();
                            continue;
                        }

                        AnsiConsole.MarkupLine("[purple]Apellido[/]");
                        string? lastName = Console.ReadLine();
                        Console.WriteLine("");
                        if (string.IsNullOrEmpty(lastName))
                        {
                            AnsiConsole.MarkupLine("[red]El apellido no puede estar vacío[/]");
                            Console.ReadLine();
                            continue;
                        }

                        AnsiConsole.MarkupLine("[purple]Dirección[/]");
                        string? address = Console.ReadLine();
                        Console.WriteLine("");
                        if (string.IsNullOrEmpty(address))
                        {
                            AnsiConsole.MarkupLine("[red]La dirección no puede estar vacía[/]");
                            Console.ReadLine();
                            continue;
                        }

                        AnsiConsole.MarkupLine("[purple]Número de DNI con letra[/]");
                        string? dni = Console.ReadLine();
                        Console.WriteLine("");
                        if (dni?.Length != 9)
                        {
                            AnsiConsole.MarkupLine("[red]DNI inválido. Tiene que tener 9 caracteres[/]");
                            continue;
                        }
                       
                        AnsiConsole.MarkupLine("[purple]Teléfono[/]");
                        string? phone = Console.ReadLine();
                        Console.WriteLine("");

                        if(string.IsNullOrEmpty(phone) || phone.Length < 9)
                        {
                            AnsiConsole.MarkupLine("[red]Entrada inválida. El teléfono es obligatorio y debe de tener al menos 9 caracteres[/]");
                            continue;
                        }
                
                        AnsiConsole.MarkupLine("[purple]Especialidad (Oftalmología/traumatología/ginecología/neurología)[/]");
                        string? area = Console.ReadLine();
                        Console.WriteLine("");

                        if (string.IsNullOrEmpty(area))
                        {
                            AnsiConsole.MarkupLine("[red]Entrada inválida. La especialidad no puede estar vacía[/]");
                            continue;
                        }

                        AnsiConsole.MarkupLine("[purple]Fecha (dd/MM/yyyy)[/]");
                        string? day = Console.ReadLine();
                        Console.WriteLine("");

                        if(day == null)
                        {
                            AnsiConsole.MarkupLine("[red]Entrada inválida. La fecha no puede estar vacía[/]");
                            continue;
                        }

                        if (!FechaValida(day))
                            {
                                AnsiConsole.MarkupLine("[red]Fecha inválida. Debe tener el formato dd/MM/yyyy[/]");
                                continue;
                            }
                        
                        AnsiConsole.MarkupLine("[purple]Hora (HH:mm)[/]");
                        string? time = Console.ReadLine();
                        Console.WriteLine("");

                        if (time == null)
                        {
                            AnsiConsole.MarkupLine("[red]Entrada inválida. La hora no puede estar vacía[/]");
                            continue;
                        }

                        if (!HoraValida(time))
                        {
                            AnsiConsole.MarkupLine("[red]Hora inválida. Debe tener el formato HH:mm[/]");
                            continue;
                        }

                        bool isUrgent;

                        AnsiConsole.MarkupLine("[purple]¿Es urgente? (si/no)[/]");
                        string? isUrgentInput = Console.ReadLine();

                        if (isUrgentInput == "no")
                        {
                            isUrgent = false;
                        }
                        else if (isUrgentInput == "si")
                        {
                            isUrgent = true;
                        }
                        else
                        {
                            AnsiConsole.MarkupLine("[red]Respuesta incorrecta. Escribe si o no[/]");
                            continue;
                        }

                        appointmentPatientService.CreateAppointmentPatient(name, lastName, address, dni, phone, area, day, time, isUrgent);
                        Console.WriteLine("");
                        AnsiConsole.MarkupLine($"[green]CITA REGISTRADA CORRECTAMENTE[/]");
                        break;
                    
                    case "2":
                        AnsiConsole.MarkupLine("[purple]Ingrese el DNI del paciente:[/]");
                        string? patientDni = Console.ReadLine();

                        if (!string.IsNullOrEmpty(patientDni))
                        {
                            var appointmentPatients = appointmentPatientService.GetAppointmentPatientsByDNI(patientDni);

                            if(appointmentPatients.Count == 0)
                            {
                                Console.WriteLine("");
                                AnsiConsole.MarkupLine($"[red]No se encontraron citas para el DNI {patientDni}[/]");
                            }
                            else
                            {
                                foreach (var appointmentPatient in appointmentPatients)
                                {
                                    Console.WriteLine("");
                                    AnsiConsole.MarkupLine("[bold invert yellow1]DATOS CITA[/]");
                                    Console.WriteLine("");
                                    Console.WriteLine($"Fecha y hora de registro: {appointmentPatient.Date}");
                                    Console.WriteLine($"Id cita: {appointmentPatient.Id}");
                                    Console.WriteLine($"Paciente: {appointmentPatient.PatientName} {appointmentPatient.PatientLastName}");
                                    Console.WriteLine($"Especialidad: {appointmentPatient.Area}");
                                    Console.WriteLine($"Fecha de la cita: {appointmentPatient.Day}");
                                    Console.WriteLine($"Hora de la cita: {appointmentPatient.Time}");
                                    Console.WriteLine($"¿Es urgente?: {(appointmentPatient.IsUrgent ? "si" : "no")}");
                                    Console.WriteLine(""); 
                                }
                            }
                        }
                        break;

                    case "3":
                        AnsiConsole.MarkupLine("[purple]Introduce un nombre de usuario[/]");
                        string? inputUserName = Console.ReadLine();
                        Console.WriteLine("");

                        if(string.IsNullOrEmpty(inputUserName))
                        {
                            AnsiConsole.MarkupLine("[red]Entrada inválida. Nombre de usuario no puede estar vacío[/]");
                            continue;
                        }
                        
                        AnsiConsole.MarkupLine("[purple]Introduce una contraseña[/]");
                        string? inputPassword = Console.ReadLine();
                        Console.WriteLine("");

                        if(inputPassword != null)
                        {
                            if (!inputPassword.Any(char.IsUpper) || (!inputPassword.Any(char.IsDigit)))
                            {
                                AnsiConsole.MarkupLine("[red]Formato de contraseña inválido. Debe contener al menos una mayúscula y un número[/]");
                                continue;
                            }
                        }
                    
                        AnsiConsole.MarkupLine("[purple]Introduce la clave de acceso para médicos:[/]");
                        string? inputAccessKey = Console.ReadLine();
                        Console.WriteLine("");

                        const string MedicalAccessKey = "medico";

                        if (string.IsNullOrEmpty(inputAccessKey) || inputAccessKey != MedicalAccessKey)
                        {
                            AnsiConsole.MarkupLine("[red]Clave de acceso incorrecta o vacía. Registro denegado[/]");
                            continue;
                        }

                        AnsiConsole.MarkupLine("[purple]Introduce un email[/]");
                        string? inputEmail = Console.ReadLine();
                        Console.WriteLine("");
                        if(inputEmail != null)
                        {
                            if (!inputEmail.Contains('@') || !inputEmail.Contains(".com"))
                            {
                                AnsiConsole.MarkupLine("[red]Formato de email inválido. Debe contener '@' y '.com'[/]");
                                continue;
                            }
                        }

                        var existUser = userService.GetUserByUserName(inputUserName);
                        if (existUser != null)
                        {
                            AnsiConsole.MarkupLine("[red]Ya existe ese nombre de usuario[/]");
                            return;
                        }
                        if (inputUserName != null && inputPassword != null && inputEmail != null && inputAccessKey != null)
                        {
                            userService.CreateUser(inputUserName, inputPassword, inputEmail, inputAccessKey);
                            AnsiConsole.MarkupLine("[green]Usuario registrado correctamente[/]");
                            break;
                        }
                        else
                        {
                            AnsiConsole.MarkupLine("[red]Uno o más de los valores de entrada son nulos. No se puede registrar el usuario[/]");
                        }

                        break;

                    case "4":
                        AnsiConsole.MarkupLine("[yellow1]Introduzca usuario y contraseña para entrar en la zona privada como personal médico[/]");
                        Console.WriteLine(" ");

                        AnsiConsole.MarkupLine("[purple]Usuario:[/]");
                        string? userName = Console.ReadLine();

                        AnsiConsole.MarkupLine("[purple]Contraseña:[/]");
                        string? password = Console.ReadLine();
                        Console.WriteLine("");

                        if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
                        {
                            AnsiConsole.MarkupLine("[red]Usuario o contraseña no válidos. Ambos campos son obligatorios[/]");
                            continue;  
                        }

                        bool authenticated = privateAreaAccess.Authentication(userName, password);

                        if (authenticated)
                        {
                            privateZone = true;
                        }
                        else
                        {
                            AnsiConsole.MarkupLine("[red]Acceso denegado[/]");
                        }
                        break;

                    default:
                        AnsiConsole.MarkupLine("[red]Opción no válida[/]");
                        break;
                }
            }
            Console.WriteLine("");
            AnsiConsole.MarkupLine("[blue]>> Presione enter para continuar[/]");
            Console.ReadLine();
            
        } while(choice != "e");    
    }
    
        //Comprobación entradas fecha y hora
        static bool HoraValida(string hora)
        {
            return DateTime.TryParseExact(hora, "HH:mm", null, System.Globalization.DateTimeStyles.None, out _);
        }

        static bool FechaValida(string fecha)
        {
            return DateTime.TryParseExact(fecha, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out _);
        }
}
