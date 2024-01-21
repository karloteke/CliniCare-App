using CliniCareApp.Models;
using CliniCareApp.Business;
using CliniCareApp.Data;

namespace CliniCareApp
{
    class Program
    {
        static void Main()
        {
            // Instancia pacientes
            IPatientRepository patientRepository = new PatientRepository();
            IPatientService patientService = new PatientService(patientRepository);

            // Instancia citas
            IAppointmentRepository appointmentRepository = new AppointmentRepository(patientRepository);
            IAppointmentService appointmentService = new AppointmentService(appointmentRepository); 

            // Instancia historial médico
            IMedicalRecordRepository medicalRecordRepository = new MedicalRecordRepository(patientRepository); 

            IMedicalRecordService medicalRecordService = new MedicalRecordService(medicalRecordRepository);

            // Instancia citas creadas por pacientes
            IAppointmentPatientRepository appointmentPatientRepository = new AppointmentPatientRepository(patientRepository);
            IAppointmentPatientService appointmentPatientService = new AppointmentPatientService(appointmentPatientRepository); 

            // Instacia Usuarios
            IUserRepository userRepository = new UserRepository();
            IUserService userService = new UserService(userRepository);

            // Instancia acceso a la zona privada
            PrivateAreaAccess privateAreaAccess = new PrivateAreaAccess(userService);

            //Llamo al método Run del menú y paso las instancias
            Menu.Run(
            patientService,
            appointmentService,
            medicalRecordService,
            appointmentPatientService,
            userService,
            privateAreaAccess);
        }        
    }
}
