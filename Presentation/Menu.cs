using ClinicApp.Models;
using ClinicApp.Business;

class Menu
{
    private static string? choice = "";

    public static void Main()
    {
        bool privateZone = false;
        PrivateAreaAccess authentication = new PrivateAreaAccess();
        
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
                Console.WriteLine("2. Visualizar historial médico");
                Console.WriteLine("3. Ir a zona privada");  
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
                        Patient.CreatePatient();
                        break;

                    case "1.1":
                        Patient.ViewPatients();
                        break;

                    case "2":
                        //Patient.CreateAppointment();
                        break;   

                    case "2.1":
                        //Patient.ViewAppointment();
                        break;

                    case "3":
                        //Patient.CreateMedicalRecords();
                        break;   

                    case "3.1":
                        //Patient.ViewMedicalRecords();
                        break;

                    case "4":
                        //Patient.SearchByDni();
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
                        //Patient.CreateAppointment();
                        break;
                    
                    case "2":
                        //Patient.ViewMedicalRecords();
                        break;

                    case "3":
                        Console.WriteLine("Para acceder a la zona privada introduzca usuario y contraseña");
                        Console.WriteLine(" ");

                        Console.Write("Usuario: ");
                        string? userName = Console.ReadLine();

                        Console.Write("Contraseña: ");
                        string? password = Console.ReadLine();
                        Console.Write("");

                        if(userName != null && password != null)
                        {
                            bool authenticated = authentication.Authentication(userName, password);

                            if(authenticated)
                            {
                                privateZone = true;
                                Console.WriteLine("");
                                Console.WriteLine("BIENVENIDO A LA ZONA PRIVADA DE CLINICARE");
                            }
                            else
                            {
                                Console.WriteLine(" ");
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
