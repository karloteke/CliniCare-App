using ClinicApp.Models;

class Menu
{
    private static string? choice = "";

    public static void Main()
    {
        bool privateZone = false;
        
        do
        {
            Console.WriteLine("=== Menú Principal CliniCare ===");
            Console.WriteLine(" ");
            Console.WriteLine(" ");

            if(privateZone)
            {
                Console.WriteLine(" === ZONA PRIVADO ===");
                Console.WriteLine(" ");
                Console.WriteLine("1. Insertar Pacientes");
                Console.WriteLine("   1.1 Visualizar pacientes");
                Console.WriteLine("2. Insertar citas médicas");
                Console.WriteLine("   2.1 Visualizar citas médicas");
                Console.WriteLine("3. Insertar historial médico");
                Console.WriteLine("   3.1 Visualizar historial médico");
                Console.WriteLine("4. Busqueda de paciente por Dni");
                Console.WriteLine("5. Ir a zona pública");
                Console.WriteLine("e Salir");
                Console.WriteLine(" ");
            }
            else
            {
                Console.WriteLine("=== ZONA PUBLICA ===");
                Console.WriteLine("1. Pedir cita");
                Console.WriteLine("2. Visualizar historial médico");
                Console.WriteLine("3. Ir a zona privada");  
                Console.WriteLine("e Salir");
                Console.WriteLine(" ");
            }

            
            string? choice = Console.ReadLine(); // Para introducir el número
      

            if(privateZone){
                
                switch (choice)
                {
                    case "1":
                        //Patient.CreatePatient();
                        break;

                    case "1.2":
                        //Patient.ViewPatients();
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

                    case "e":
                        Console.WriteLine("¡Hasta la próxima!");
                        break;

                    default:
                        Console.WriteLine("Opción no válida.");
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
                        privateZone = true;
                        break;

                    case "e":
                        Console.WriteLine("¡Hasta la próxima!");
                        break;
                        
                    default:
                        Console.WriteLine("Opción no válida.");
                        break;
                
                }               
            }
            Console.WriteLine("");
            Console.WriteLine("Presione enter para continuar...");
            Console.ReadLine();
        } while(choice != "e"); 
    } 
}
