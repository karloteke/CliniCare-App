using Microsoft.AspNetCore.Mvc;
using CliniCareApp.Business;
using CliniCareApp.Models;
using Microsoft.AspNetCore.Authorization; 

namespace CliniCareApp.API.Controllers;

[ApiController]
[Route("[controller]")] 
[Authorize]
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


    [HttpGet(Name = "GetAllAppointments")] 
    public ActionResult<IEnumerable<Appointment>> SearchAppointments(string? patientDni, string? area, string? medicalName, bool orderByUrgentAsc = false)
    {
        var query = _appointmentService.GetAllAppointments().AsQueryable();

        if (!string.IsNullOrWhiteSpace(patientDni))
        {
            query = query.Where(a => a.PatientDni.Contains(patientDni));
        }

        if (!string.IsNullOrWhiteSpace(area))
        {
            query = query.Where(a => a.Area.Contains(area));
        }

        if (!string.IsNullOrWhiteSpace(medicalName))
        {
            query = query.Where(a => a.MedicalName.Contains(medicalName));
        }

        if (orderByUrgentAsc)
        {
            query = query.OrderByDescending(a => a.IsUrgent);
        }
        else
        {
            query = query.OrderBy(a => a.IsUrgent);
        }

        var appointments = query.ToList();

        if (appointments.Count == 0)
        {
            return NotFound();
        }

        return appointments;
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

    [HttpPost]
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
