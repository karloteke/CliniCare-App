// using Microsoft.AspNetCore.Mvc;

// using CliniCareApp.Data;
// using CliniCareApp.Business;
// using CliniCareApp.Models;

// namespace CliniCareApp.API.Controllers;

// [ApiController]
// [Route("[controller]")] 
// public class AppointmentPatientsController : ControllerBase
// {
//     private readonly ILogger<AppointmentPatientsController> _logger;
//     private readonly IAppointmentPatientService _appointmentPatientService;
//     private readonly IPatientService _patientService;
//     private readonly IAppointmentService _appointmentService;
//     private readonly IAppointmentRepository _appointmentRepository;

//     public AppointmentPatientsController(ILogger<AppointmentPatientsController> logger, IAppointmentPatientService appointmentPatientService,  IPatientService patientService, IAppointmentService appointmentService, IAppointmentRepository appointmentRepository)
//     {
//         _logger = logger;
//         _appointmentPatientService = appointmentPatientService;
//         _patientService = patientService;
//         _appointmentService = appointmentService;
//         _appointmentRepository = appointmentRepository;
//     }

//     [HttpGet(Name = "GetAppointmentsByFilters")] 
//     public ActionResult<IEnumerable<Appointment>> SearchAppointmentsByFilters(string patientDni)
//     {
//         var query = _appointmentService.GetAllAppointments().AsQueryable();

//         if (!string.IsNullOrWhiteSpace(patientDni))
//         {
//             query = query.Where(p => p.PatientDni.Contains(patientDni));
//         }

//             var appointments = query.ToList();
//             return appointments;
//     }




//     // // GET: /Appointments/{patientDni}
//     // [HttpGet("{dni}", Name = "GetAppointmentPatientByDni")]
//     // public IActionResult GetAppointmentPatientByDni(string dni)
//     // {
//     //     try
//     //     {
//     //         var appointment = _appointmentService.GetAppointments(dni);
//     //         return Ok(appointment);     
//     //     }
//     //     catch (KeyNotFoundException)
//     //     {
//     //         return NotFound($"No hay citas para el paciente con el DNI: {dni}");
//     //     }
//     // }
    
//     [HttpPost]
//     public IActionResult NewAppointmentPatient([FromBody] AppointmentCreateDTO appointmentDto, [FromQuery] string patientDni)
//     {
//         try 
//         {
//             // Verificar si el modelo recibido es válido
//             if (!ModelState.IsValid)
//             {
//                 return BadRequest(ModelState);
//             }

//             var patient = _patientService.GetPatientByDni(patientDni);
//             if(patient == null)
//             {
//                 return NotFound ($"No existe el DNI: {patientDni} en nuestra base de datos, registrese antes en la sección pacientes");
//             }

//             _appointmentService.CreateAppointment(patientDni, appointmentDto.CreatedAt, appointmentDto.Area, appointmentDto.MedicalName, appointmentDto.Date, appointmentDto.Time, appointmentDto.IsUrgent);
//             return Ok($"Se ha creado correctamente la cita para el paciente con DNI: {patientDni}");
//         }     
//         catch (Exception ex)
//         {
//             return BadRequest(ex.Message);
//         }
//     }


//     //PUT: /Appointments/{id}
//     [HttpPut("{appointmentId}")]
//     public IActionResult UpdateAppointment(int appointmentId, [FromBody] AppointmentUpdateDTO appointmentDto)
//     {
//         if (!ModelState.IsValid)  {return BadRequest(ModelState); } 

//         try
//         {
//             _appointmentService.UpdateAppointmentDetails(appointmentId, appointmentDto);
//             return Ok($"La cita con Id: {appointmentId} ha sido actualizada correctamente");
//         }
//         catch (KeyNotFoundException)
//         {
//            return NotFound();
//         }
//     }


//     // DELETE: /Appointment/{AppointmentId}
//     [HttpDelete("{appointmentId}")]
//     public IActionResult DeleteAppointment(int appointmentId)
//     {
//         try
//         {
//             _appointmentService.DeleteAppointment(appointmentId);
//             return NoContent();
//         }
//         catch (KeyNotFoundException ex)
//         {
//             _logger.LogInformation(ex.Message);
//             return NotFound();
//         }
//     }

// }
