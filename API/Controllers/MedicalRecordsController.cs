using Microsoft.AspNetCore.Mvc;
using CliniCareApp.Business;
using CliniCareApp.Models;
using Microsoft.AspNetCore.Authorization; 

namespace CliniCareApp.API.Controllers;

[ApiController]
[Route("[controller]")] 
// [Authorize]
public class MedicalRecordsController : ControllerBase
{
    private readonly ILogger<MedicalRecordsController> _logger;
    private readonly IMedicalRecordService _medicalRecordService;
    private readonly IPatientService _patientService;
    private readonly IAppointmentService _appointmentService;

    public MedicalRecordsController(ILogger<MedicalRecordsController> logger, IMedicalRecordService medicalRecordService, IPatientService patientService)
    {
        _logger = logger;
        _medicalRecordService = medicalRecordService;
        _patientService = patientService;
    }

    [Authorize(Roles = Roles.Admin)]
    [HttpGet(Name = "GetAllMedicalRecords")] 
    public ActionResult<IEnumerable<MedicalRecord>> GetAllMedicalRecords([FromQuery] MedicalRecordQueryParameters medicalRecordQueryParameters)
    {
        if (!ModelState.IsValid)  {return BadRequest(ModelState); } 

        try 
        {
            var medicalRecords = _medicalRecordService.GetAllMedicalRecords(medicalRecordQueryParameters);
            
                if (medicalRecords == null || !medicalRecords.Any())
                    {
                        return NotFound("No hay citas disponibles.");
                    }
                    
            return Ok(medicalRecords);
        }     
        catch (Exception ex)
        {
            return BadRequest(ex);
        }
    }


    [Authorize(Roles = Roles.Admin)]
    [HttpGet("{medicalRecordId}", Name = " GetMedicalRecordById")]
    public IActionResult  GetMedicalRecordById(int medicalRecordId)
    {
        try
        {
            var medicalRecord = _medicalRecordService. GetMedicalRecordById(medicalRecordId);
            return Ok(medicalRecord);     
        }
        catch (KeyNotFoundException)
        {
            return NotFound($"No existe historial médico para el paciente con el Id: {medicalRecordId}");
        }
    }

    [Authorize(Roles = Roles.Admin)]
    [HttpPost]
    public IActionResult  NewMedicalRecord([FromBody] MedicalRecordCreateDTO medicalRecordDto, [FromQuery] string patientDni)
    {
        try 
        {
            // Verificar si el modelo recibido es válido
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (string.IsNullOrEmpty(medicalRecordDto.DoctorName) || string.IsNullOrEmpty(medicalRecordDto.Treatment)||
            string.IsNullOrEmpty(medicalRecordDto.Notes))

            {
                return BadRequest("Los campos no pueden estar vacíos.");
            }

            var patient = _patientService.GetPatientByDni(patientDni);
            if(patient == null)
            {
                return NotFound ("No existe ese DNI");
            }

            _medicalRecordService.CreateMedicalRecord(patientDni, medicalRecordDto.CreatedAt, medicalRecordDto.DoctorName, medicalRecordDto.Treatment, medicalRecordDto.TreatmentCost,  medicalRecordDto.Notes);
            return Ok($"Se ha creado correctamente la cita para el paciente con DNI: {patientDni}");
        }     
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }


    [Authorize(Roles = Roles.Admin)]
    [HttpPut("{medicalRecordId}")]
    public IActionResult UpdateMedicalRecord(int medicalRecordId, [FromBody] MedicalRecordUpdateDTO medicalRecordDto)
    {
        if (!ModelState.IsValid)  {return BadRequest(ModelState); } 

        try
        {
            _medicalRecordService.UpdateMedicalRecordDetails(medicalRecordId, medicalRecordDto);
            return Ok($"El historial médico con Id: {medicalRecordId} ha sido actualizada correctamente");
        }
        catch (KeyNotFoundException)
        {
           return NotFound();
        }
    }

    [Authorize(Roles = Roles.Admin)]
    [HttpDelete("{medicalRecordId}")]
    public IActionResult DeleteMedicalRecord(int medicalRecordId)
    {
        try
        {
            _medicalRecordService.DeleteMedicalRecord(medicalRecordId);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            _logger.LogInformation(ex.Message);
            return NotFound();
        }
    }
}
