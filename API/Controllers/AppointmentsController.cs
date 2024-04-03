using Microsoft.AspNetCore.Mvc;
using CliniCareApp.Business;
using CliniCareApp.Models;
using Microsoft.AspNetCore.Authorization; 

namespace CliniCareApp.API.Controllers;

[ApiController]
[Route("[controller]")] 
// [Authorize]
public class AppointmentsController : ControllerBase
{
    private readonly ILogger<AppointmentsController> _logger;
    private readonly IAppointmentService _appointmentService;
    private readonly IPatientService _patientService;

    public AppointmentsController(ILogger<AppointmentsController> logger, IAppointmentService appointmentService, IPatientService patientService)
    {
        _logger = logger;
        _appointmentService = appointmentService;
        _patientService = patientService;
    }

    // [Authorize]
    //Zona privada donde la Clínica puede ver todas las citas y filtrar por varios campos.
    [HttpGet("PrivateZone", Name = "GetAllAppointments")] 
    public ActionResult<IEnumerable<Appointment>> GetAllAppointments([FromQuery] AppointmentQueryParameters appointmentQueryParameters, bool orderByUrgentAsc)
    {
        if (!ModelState.IsValid)  {return BadRequest(ModelState); } 

        try 
        {
            var appointments = _appointmentService.GetAllAppointments(appointmentQueryParameters, orderByUrgentAsc);
            
                if (appointments == null || !appointments.Any())
                    {
                        return NotFound("No hay citas disponibles.");
                    }
                    
            return Ok(appointments);
        }     
        catch (Exception ex)
        {
            return BadRequest(ex);
        }
    }


    // Zona pública donde los pacientes pueden ver con el DNI sus citas 
    [HttpGet("PublicZone", Name = "GetAppointmentsForPatient")] 
    public ActionResult<IEnumerable<Appointment>> GetAppointmentsForPatient([FromQuery] AppointmentPatientQueryParameters appointmentPatientQueryParameters, bool orderByDateAsc)
    {
        if (!ModelState.IsValid)  {return BadRequest(ModelState); } 

        try 
        {
            var appointments = _appointmentService.GetAppointmentsForPatient(appointmentPatientQueryParameters, orderByDateAsc);
            
                if (appointments == null || !appointments.Any())
                    {
                        return NotFound("No hay citas disponibles.");
                    }
                    
            return Ok(appointments);
        }     
        catch (Exception ex)
        {
            return BadRequest(ex);
        }
    }


    // // GET: /Appointments/{id}
    // [HttpGet("{appointmentId}", Name = " GetAppointmentById")]
    // public IActionResult  GetAppointmentById(int appointmentId)
    // {
    //     try
    //     {
    //         var appointment = _appointmentService. GetAppointmentById(appointmentId);
    //         return Ok(appointment);     
    //     }
    //     catch (KeyNotFoundException)
    //     {
    //         return NotFound($"No existe la cita para el paciente con el Id {appointmentId}");
    //     }
    // }



    [HttpPost("PublicZone")]
    public IActionResult NewAppointment([FromBody] AppointmentCreateDTO appointmentDto, [FromQuery] string patientDni)
    {
        try 
        {
            // Verificar si el modelo recibido es válido
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var patient = _patientService.GetPatientByDni(patientDni);
            if(patient == null)
            {
                return NotFound ("No existe ese DNI");
            }

            _appointmentService.CreateAppointment(patientDni, appointmentDto.CreatedAt, appointmentDto.Area, appointmentDto.MedicalName, appointmentDto.Date, appointmentDto.Time, appointmentDto.IsUrgent);
            return Ok($"Se ha creado correctamente la cita para el paciente con DNI: {patientDni}");
        }     
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [Authorize]
    //PUT: /Appointments/{id}
    [HttpPut("{appointmentId}")]
    public IActionResult UpdateAppointment(int appointmentId, [FromBody] AppointmentUpdateDTO appointmentDto)
    {
        if (!ModelState.IsValid)  {return BadRequest(ModelState); } 

        try
        {
            _appointmentService.UpdateAppointmentDetails(appointmentId, appointmentDto);
            return Ok($"La cita con Id: {appointmentId} ha sido actualizada correctamente");
        }
        catch (KeyNotFoundException)
        {
           return NotFound();
        }
    }

    [Authorize]
    // DELETE: /Appointment/{AppointmentId}
    [HttpDelete("{appointmentId}")]
    public IActionResult DeleteAppointment(int appointmentId)
    {
        try
        {
            _appointmentService.DeleteAppointment(appointmentId);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            _logger.LogInformation(ex.Message);
            return NotFound();
        }
    }   
}
