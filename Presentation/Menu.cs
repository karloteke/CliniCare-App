using CliniCareApp.Models;
using CliniCareApp.Business;
using CliniCareApp.Data;
using Spectre.Console;
using System.Text.RegularExpressions;

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
        string patientDni;
        DateTime medicalRecordDate;
        DateTime appointmentDate;
        Patient? patient;
    

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

                         if (dni is null)
                        {
                            AnsiConsole.MarkupLine("[red]DNI inválido. No puede estar vacio[/]");
                            continue;
                        }

                        // Expresión que valida 8 dígitos seguidos de una letra (no sensible a mayúsculas/minúsculas)
                        Regex dniRegex = new Regex(@"^\d{8}[a-zA-Z]$");

                        if (!dniRegex.IsMatch(dni))
                        {
                            AnsiConsole.MarkupLine("[red]DNI inválido. Debe tener 8 dígitos seguidos de una letra[/]");
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

                        PatientCreateDTO patientDto = new PatientCreateDTO
                        {
                            Name = name,
                            LastName = lastName,
                            Address = address,
                            Dni = dni,
                            Phone = phone
                        };
                    
                        patientService.CreatePatient(name, lastName, address, dni, phone);
                        AnsiConsole.MarkupLine("[green]PACIENTE REGISTRADO CORRECTAMENTE[/]");
                        break;

                    case "1.1":
                        var patients = patientService.GetAllPatients();
                        if (patients.Any())
                        {
                            foreach (var p in patients)
                            {
                                AnsiConsole.MarkupLine("[bold invert darkolivegreen1_1]DATOS PACIENTE[/]");
                                Console.WriteLine("");
                                AnsiConsole.MarkupLine($"[darkslategray2]Id:[/] {p.Id}");
                                AnsiConsole.MarkupLine($"[darkslategray2]Nombre:[/] {p.Name}");
                                AnsiConsole.MarkupLine($"[darkslategray2]Apellido:[/] {p.LastName}");
                                AnsiConsole.MarkupLine($"[darkslategray2]Dirección:[/] {p.Address}");
                                AnsiConsole.MarkupLine($"[darkslategray2]Dni:[/] {p.Dni}");
                                AnsiConsole.MarkupLine($"[darkslategray2]Teléfono:[/] {p.Phone}");
                                Console.WriteLine("");
                            }
                        }
                        else
                        {
                            AnsiConsole.MarkupLine("[bold red]No existen pacientes[/]");
                        }
                        break;

                    case "2":
                        appointmentDate = DateTime.Now;
                        AnsiConsole.MarkupLine($"[purple]Fecha y hora de creación:[/] {appointmentDate}");

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
                        AnsiConsole.MarkupLine ("[purple]Introduce el DNI del paciente al que quieres asignarle la cita[/]");
                        string? patientDniInput = Console.ReadLine();


                        patient = patientService.GetPatientByDni(patientDniInput);

                        if (patient != null)
                        {
                            appointmentService.CreateAppointment(patientDniInput, appointmentDate, area, medicalName, date, time, isUrgent);
                            Console.WriteLine("");
                            AnsiConsole.MarkupLine($"[green]Cita registrada correctamente para: {patient?.Name} {patient?.LastName} con DNI: {patient?.Dni}[/]");
                        }
                        else
                        {
                            AnsiConsole.MarkupLine("[red]No se encontró un paciente con el DNI proporcionado[/]");
                        }
                        break; 

                    case "2.1":
                        var appointments = appointmentService.GetAllAppointments();

                        if(appointments.Any())
                        {
                            foreach (var appointment in appointments)
                            {
                                var patientObject = patientService.GetPatientByDni(appointment.PatientDni);

                                AnsiConsole.MarkupLine("[bold invert darkolivegreen1_1]DATOS CITA[/]");
                                Console.WriteLine("");
                                AnsiConsole.MarkupLine($"[darkslategray2]Fecha y hora de registro:[/] {appointment.CreatedAt}"); 
                                AnsiConsole.MarkupLine($"[darkslategray2]Id cita:[/] {appointment.Id}");
                                AnsiConsole.MarkupLine($"[darkslategray2]Paciente:[/] {patientObject?.Name} {patientObject?.LastName}");
                                AnsiConsole.MarkupLine($"[darkslategray2]Especialidad:[/] {appointment.Area}");
                                AnsiConsole.MarkupLine($"[darkslategray2]Nombre médico:[/]{appointment.MedicalName}");
                                AnsiConsole.MarkupLine($"[darkslategray2]Hora:[/] {appointment.Time}");
                                AnsiConsole.MarkupLine($"[darkslategray2]Día:[/] {appointment.Date}");
                                AnsiConsole.MarkupLine($"[darkslategray2]¿Es urgente?:[/] {(appointment.IsUrgent ? "si" : "no")}");
                                Console.WriteLine(""); 
                            }
                        }
                        else
                        {
                            AnsiConsole.MarkupLine("[bold red]No existe ninguna cita[/]");
                        }    
                        break;

                    case "3":
                        medicalRecordDate = DateTime.Now;
                        AnsiConsole.MarkupLine($"[purple]Fecha y hora de creación:[/] {medicalRecordDate}");

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
                        AnsiConsole.MarkupLine("[purple]Introduce el DNI del paciente al que quieres asignarle el informe médico[/]");
                        string? patientDniMedicalRecord = Console.ReadLine();

                        if (string.IsNullOrEmpty(patientDniMedicalRecord))
                        {
                            AnsiConsole.MarkupLine("[red]Entrada inválida. Debes ingresar un DNI de paciente[/]");
                            continue;
                        }

                        // Obtener el paciente por su DNI
                        var patientFromService = patientService.GetPatientByDni(patientDniMedicalRecord);

                        if (patientFromService != null)
                        {
                            medicalRecordService.CreateMedicalRecord(patientDniMedicalRecord, medicalRecordDate, doctorName, treatment, treatmentCost, notes);
                            Console.WriteLine("");
                            AnsiConsole.MarkupLine($"[green]Historial médico registrado correctamente para: {patientFromService.Name} {patientFromService.LastName}[/]");
                        }
                        else
                        {
                            AnsiConsole.MarkupLine("[red]No se encontró un paciente con el DNI proporcionado[/]");
                        }
                        break;

                    case "3.1":
                        var medicalRecords = medicalRecordService.GetAllMedicalRecords();

                        if(medicalRecords.Any())
                        {
                            foreach (var medicalRecord in medicalRecords)
                            {
                                // Obtener el DNI del paciente asociado al historial médico
                                string? dniPatient = medicalRecord.PatientDni;
                                var objectPatient = patientService.GetPatientByDni(dniPatient);

                                AnsiConsole.MarkupLine("[bold invert darkolivegreen1_1]DATOS HISTORIAL MÉDICO[/]");
                                Console.WriteLine("");
                                AnsiConsole.MarkupLine($"[darkslategray2]Fecha y hora de registro:[/] {medicalRecord.CreatedAt}");
                                AnsiConsole.MarkupLine($"[darkslategray2]Id historial médico:[/] {medicalRecord.Id}");
                                AnsiConsole.MarkupLine($"[darkslategray2]Paciente:[/] {objectPatient?.Name} {objectPatient?.LastName}");
                                AnsiConsole.MarkupLine($"[darkslategray2]Nombre médico:[/] {medicalRecord.DoctorName}");
                                AnsiConsole.MarkupLine($"[darkslategray2]Tratamiento:[/] {medicalRecord.Treatment}");
                                AnsiConsole.MarkupLine($"[darkslategray2]Coste del tratamiento:[/] {medicalRecord.TreatmentCost}");
                                AnsiConsole.MarkupLine($"[darkslategray2]Notas:[/] {medicalRecord.Notes}");
                                Console.WriteLine(""); 
                            }
                        }
                        else
                        {
                            AnsiConsole.MarkupLine("[bold red]No existe ningún historial médico[/]");
                        }
                        break;

                    case "4":
                        AnsiConsole.MarkupLine("[purple]Ingrese el DNI del paciente a buscar:[/]");
                        string? dniToSearch = Console.ReadLine();
                        Console.WriteLine("");

                        if (!string.IsNullOrEmpty(dniToSearch))
                        {
                            // Llamar al método de búsqueda del service
                            Patient? foundPatient = patientService.SearchByDni(dniToSearch);

                            if (foundPatient != null)
                            {
                                AnsiConsole.MarkupLine("[bold invert darkolivegreen1_1]DATOS DE PACIENTE ENCONTRADO[/]");
                                Console.WriteLine("");
                                AnsiConsole.MarkupLine($"[darkslategray2]Id:[/] {foundPatient.Id}");
                                AnsiConsole.MarkupLine($"[darkslategray2]Nombre:[/] {foundPatient.Name}");
                                AnsiConsole.MarkupLine($"[darkslategray2]Apellido:[/] {foundPatient.LastName}");
                                AnsiConsole.MarkupLine($"[darkslategray2]Dirección:[/] {foundPatient.Address}");
                                AnsiConsole.MarkupLine($"[darkslategray2]Dni:[/] {foundPatient.Dni}");
                                AnsiConsole.MarkupLine($"[darkslategray2]Teléfono:[/] {foundPatient.Phone}");
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
                        string? newName = Console.ReadLine();
                        Console.WriteLine("");
                        if (string.IsNullOrEmpty(newName))
                        {
                            AnsiConsole.MarkupLine("[red]El nombre no puede estar vacío[/]");
                            Console.ReadLine();
                            continue;
                        }

                        AnsiConsole.MarkupLine("[purple]Apellido[/]");
                        string? newLastName = Console.ReadLine();
                        Console.WriteLine("");
                        if (string.IsNullOrEmpty(newLastName))
                        {
                            AnsiConsole.MarkupLine("[red]El apellido no puede estar vacío[/]");
                            Console.ReadLine();
                            continue;
                        }

                        AnsiConsole.MarkupLine("[purple]Dirección[/]");
                        string? newAddress = Console.ReadLine();
                        Console.WriteLine("");
                        if (string.IsNullOrEmpty(newAddress))
                        {
                            AnsiConsole.MarkupLine("[red]La dirección no puede estar vacía[/]");
                            Console.ReadLine();
                            continue;
                        }

                        AnsiConsole.MarkupLine("[purple]Número de DNI con letra[/]");
                        string? newDni = Console.ReadLine();
                        Console.WriteLine("");

                                                
                        if (newDni is null)
                        {
                            AnsiConsole.MarkupLine("[red]DNI inválido. No puede estar vacio[/]");
                            continue;
                        }

                        // Expresión que valida 8 dígitos seguidos de una letra (no sensible a mayúsculas/minúsculas)
                        Regex dniRegex = new Regex(@"^\d{8}[a-zA-Z]$");

                        if (!dniRegex.IsMatch(newDni))
                        {
                            AnsiConsole.MarkupLine("[red]DNI inválido. Debe tener 8 dígitos seguidos de una letra[/]");
                            continue;
                        }
                        
                       
                        AnsiConsole.MarkupLine("[purple]Teléfono[/]");
                        string? newPhone = Console.ReadLine();
                        Console.WriteLine("");

                        if(string.IsNullOrEmpty(newPhone) || newPhone.Length < 9)
                        {
                            AnsiConsole.MarkupLine("[red]Entrada inválida. El teléfono es obligatorio y debe de tener al menos 9 caracteres[/]");
                            continue;
                        }
                
                        AnsiConsole.MarkupLine("[purple]Especialidad (Oftalmología/traumatología/ginecología/neurología)[/]");
                        string? newArea = Console.ReadLine();
                        Console.WriteLine("");

                        if (string.IsNullOrEmpty(newArea))
                        {
                            AnsiConsole.MarkupLine("[red]Entrada inválida. La especialidad no puede estar vacía[/]");
                            continue;
                        }

                        AnsiConsole.MarkupLine("[purple]Fecha (dd/MM/yyyy)[/]");
                        string? newDay = Console.ReadLine();
                        Console.WriteLine("");

                        if(newDay == null)
                        {
                            AnsiConsole.MarkupLine("[red]Entrada inválida. La fecha no puede estar vacía[/]");
                            continue;
                        }

                        if (!FechaValida(newDay))
                            {
                                AnsiConsole.MarkupLine("[red]Fecha inválida. Debe tener el formato dd/MM/yyyy[/]");
                                continue;
                            }
                        
                        AnsiConsole.MarkupLine("[purple]Hora (HH:mm)[/]");
                        string? newTime = Console.ReadLine();
                        Console.WriteLine("");

                        if (newTime == null)
                        {
                            AnsiConsole.MarkupLine("[red]Entrada inválida. La hora no puede estar vacía[/]");
                            continue;
                        }

                        if (!HoraValida(newTime))
                        {
                            AnsiConsole.MarkupLine("[red]Hora inválida. Debe tener el formato HH:mm[/]");
                            continue;
                        }

                        bool isUrgent;

                        AnsiConsole.MarkupLine("[purple]¿Es urgente? (si/no)[/]");
                        string? newIsUrgentInput = Console.ReadLine();

                        if (newIsUrgentInput == "no")
                        {
                            isUrgent = false;
                        }
                        else if (newIsUrgentInput == "si")
                        {
                            isUrgent = true;
                        }
                        else
                        {
                            AnsiConsole.MarkupLine("[red]Respuesta incorrecta. Escribe si o no[/]");
                            continue;
                        }

                          // Crear una instancia de Patient con los detalles obtenidos
                        Patient patientNew = new Patient(newName, newLastName, newAddress, newDni, newPhone);

                        // Guardar el nuevo paciente 
                        patientService.CreatePatient(newName, newLastName, newAddress, newDni, newPhone);

                        appointmentPatientService.CreateAppointmentPatient(patientNew, newArea, newDay, newTime, isUrgent);
                        Console.WriteLine("");
                        AnsiConsole.MarkupLine($"[green]CITA REGISTRADA CORRECTAMENTE[/]");
                        break;
                    
                    case "2":
                        AnsiConsole.MarkupLine("[purple]Ingrese el DNI del paciente:[/]");
                        string? inputPatientDni = Console.ReadLine();

                        if (string.IsNullOrEmpty(inputPatientDni))
                        {
                            Console.WriteLine("");
                            AnsiConsole.MarkupLine("[red]Entrada inválida. El DNI no puede estar vacio[/]");
                            break;
                        }

                        var appointmentPatients = appointmentPatientService.GetAppointmentPatientsByDNI(inputPatientDni);

                        if(appointmentPatients.Count == 0)
                        {
                            Console.WriteLine("");
                            AnsiConsole.MarkupLine($"[red]No se encontraron citas para el DNI {inputPatientDni}[/]");
                        }
                        else
                        {
                            foreach (var appointmentPatient in appointmentPatients)
                            {
                                Console.WriteLine("");
                                AnsiConsole.MarkupLine("[bold invert darkolivegreen1_1]DATOS CITA[/]");
                                Console.WriteLine("");
                                AnsiConsole.MarkupLine($"[darkslategray2]Fecha y hora de registro: [/]{appointmentPatient.Date}");
                                AnsiConsole.MarkupLine($"[darkslategray2]Id cita: [/]{appointmentPatient.Id}");
                                AnsiConsole.MarkupLine($"[darkslategray2]Paciente: [/]{appointmentPatient?.Patient?.Name} {appointmentPatient?.Patient?.LastName}");
                                AnsiConsole.MarkupLine($"[darkslategray2]Especialidad: [/]{appointmentPatient?.Area}");
                                AnsiConsole.MarkupLine($"[darkslategray2]Fecha de la cita: [/]{appointmentPatient?.Day}");
                                AnsiConsole.MarkupLine($"[darkslategray2]Hora de la cita: [/]{appointmentPatient?.Time}");
                                AnsiConsole.MarkupLine($"[darkslategray2]¿Es urgente?: [/]{(appointmentPatient.IsUrgent ? "si" : "no")}");
                                Console.WriteLine(""); 
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

                        var existUser = userService.GetUserByUserName(inputUserName);
                        if (existUser != null)
                        {
                            AnsiConsole.MarkupLine("[red]Ya existe ese nombre de usuario[/]");
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
                    
                        // AnsiConsole.MarkupLine("[purple]Introduce la clave de acceso para médicos:[/]");
                        // string? inputAccessKey = Console.ReadLine();
                        // Console.WriteLine("");

                        // const string MedicalAccessKey = "medico";

                        // if (string.IsNullOrEmpty(inputAccessKey) || inputAccessKey != MedicalAccessKey)
                        // {
                        //     AnsiConsole.MarkupLine("[red]Clave de acceso incorrecta o vacía. Registro denegado[/]");
                        //     continue;
                        // }

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

                        if (inputUserName != null && inputPassword != null && inputEmail != null)
                        {
                            userService.CreateUser(inputUserName, inputPassword, inputEmail);
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

                        User authenticated = privateAreaAccess.Authentication(userName, password);

                        if (authenticated != null)
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
