using Microsoft.AspNetCore.Mvc;

using CliniCareApp.Data;
using CliniCareApp.Business;
using CliniCareApp.Models;

namespace CliniCareApp.API.Controllers;

[ApiController]
[Route("[controller]")] 
public class AppointmentsController : ControllerBase
{
    private readonly ILogger<AppointmentsController> _logger;
    private readonly IAppointmentService _appointmentService;

    public AppointmentsController(ILogger<AppointmentsController> logger, IAppointmentService appointmentService)
    {
        _logger = logger;
        _appointmentService = appointmentService;
    }

    // GET: /Appointments
    [HttpGet(Name = "GetAllAppointments")] 
    public ActionResult<IEnumerable<Appointment>> GetAppointments()
    {
        try 
        {
            var appointments = _appointmentService.GetAllAppointments();

            if(appointments.Any())
            {
                return Ok(appointments);
            }
            else
            {
                return NotFound("No existen citas para mostrar");
            }
        }     
        catch (Exception ex)
        {
            return BadRequest(ex);
        }      
    }

    // GET: /Appointment/{id}
    [HttpGet("{appointmentId}", Name = " GetAppointmentById")]
    public IActionResult  GetAppointmentById(int appointmentId)
    {
        try
        {
            var appointment = _appointmentService. GetAppointmentById(appointmentId);
            return Ok(appointment);     
        }
        catch (KeyNotFoundException)
        {
            return NotFound($"No existe la cita para el paciente con el Id {appointmentId}");
        }
    }

    [HttpPost]
    public IActionResult NewAppointment([FromBody] AppointmentCreateDTO appointmentDto, [FromQuery] int patientId)
    {
        try 
        {
            // Verificar si el modelo recibido es válido
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _appointmentService.CreateAppointment(patientId, appointmentDto.CreatedAt, appointmentDto.Area, appointmentDto.MedicalName, appointmentDto.Date, appointmentDto.Time, appointmentDto.IsUrgent);
            return Ok($"Se ha creado correctamente la cita para el paciente con Id: {patientId}");
        }     
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    //PUT: /Appointment/{id}
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
