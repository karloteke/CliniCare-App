using Microsoft.AspNetCore.Mvc;

using CliniCareApp.Data;
using CliniCareApp.Business;
using CliniCareApp.Models;

namespace CliniCareApp.API.Controllers;

[ApiController]
[Route("[controller]")] 
public class AppointmentPatientsController : ControllerBase
{
    private readonly ILogger<AppointmentPatientsController> _logger;
    private readonly IAppointmentPatientService _appointmentPatientService;
    private readonly IPatientService _patientService;
    private readonly IAppointmentService _appointmentService;

    public AppointmentPatientsController(ILogger<AppointmentPatientsController> logger, IAppointmentPatientService appointmentPatientService,  IPatientService patientService, IAppointmentService appointmentService)
    {
        _logger = logger;
        _appointmentPatientService = appointmentPatientService;
        _patientService = patientService;
        _appointmentService = appointmentService;
    }

     [HttpGet(Name = "GetAppointmentsByFilters")] 
    public ActionResult<IEnumerable<Appointment>> SearchAppointmentsByFilters(string patientDni, bool orderByDateAsc)
    {
        var query = _appointmentService.GetAllAppointments().AsQueryable();

        if (!string.IsNullOrWhiteSpace(patientDni))
        {
            query = query.Where(p => p.PatientDni.Contains(patientDni));
        }

        if (orderByDateAsc)
        {
            query = query.OrderBy(a => a.Date);
        }
        else
        {
            query = query.OrderByDescending(p => p.Date);
        }

        var appointments = query.ToList();

        if (appointments.Count == 0)
        {
            return NotFound();
        }

        return appointments;
    }


    // // GET: /Appointments/{patientDni}
    // [HttpGet("{dni}", Name = "GetAppointmentPatientByDni")]
    // public IActionResult GetAppointmentPatientByDni(string dni)
    // {
    //     try
    //     {
    //         var appointment = _appointmentService.GetAppointments(dni);
    //         return Ok(appointment);     
    //     }
    //     catch (KeyNotFoundException)
    //     {
    //         return NotFound($"No hay citas para el paciente con el DNI: {dni}");
    //     }
    // }


    [HttpPost]
    public IActionResult NewAppointmentPatient([FromBody] AppointmentPatientCreateDTO appointmentPatientDto)
    {
        try 
        {
            // Verificar si el modelo recibido es válido
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Obtener el paciente correspondiente al DNI
            var patient = _patientService.GetPatientByDni(appointmentPatientDto.Patient.Dni);
            if (patient == null)
            {
                // Crea un nuevo paciente si no se encuentra ninguno con el DNI proporcionado
                patient = _patientService.CreatePatient(
                    appointmentPatientDto.Patient.Name,
                    appointmentPatientDto.Patient.LastName,
                    appointmentPatientDto.Patient.Address,
                    appointmentPatientDto.Patient.Dni,
                    appointmentPatientDto.Patient.Phone
                );
            }

            // Extraer detalles de la cita 
            var appointmentDto = appointmentPatientDto.Appointment;

            // Crear la cita para el paciente
            _appointmentService.CreateAppointment(
                patient.Dni,
                DateTime.Now, 
                appointmentDto.Area, 
                appointmentDto.MedicalName, 
                appointmentDto.Date, 
                appointmentDto.Time, 
                appointmentDto.IsUrgent
            );

            return Ok($"Se ha creado correctamente la cita para el paciente con DNI: {appointmentPatientDto.Patient.Dni}");
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
