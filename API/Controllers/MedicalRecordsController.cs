using Microsoft.AspNetCore.Mvc;

using CliniCareApp.Data;
using CliniCareApp.Business;
using CliniCareApp.Models;

namespace CliniCareApp.API.Controllers;

[ApiController]
[Route("[controller]")] 
public class MedicalRecordsController : ControllerBase
{
    private readonly ILogger<MedicalRecordsController> _logger;
    private readonly IMedicalRecordService _medicalRecordService;

    public MedicalRecordsController(ILogger<MedicalRecordsController> logger, IMedicalRecordService medicalRecordService)
    {
        _logger = logger;
        _medicalRecordService = medicalRecordService;
    }

    // GET: /MedicalRecords
    [HttpGet(Name = "GetAllMedicalRecords")] 
    public ActionResult<IEnumerable<Appointment>> GetMedicalRecords()
    {
        try 
        {
            var medicalRecords = _medicalRecordService.GetAllMedicalRecords();

            if(medicalRecords.Any())
            {
                return Ok(medicalRecords);
            }
            else
            {
                return NotFound("No existe ningún historial médico para mostrar");
            }
        }     
        catch (Exception ex)
        {
            return BadRequest(ex);
        }      
    }

    // GET: /MedicalRecord/{id}
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

    [HttpPost]
    public IActionResult NewMedicalRecord([FromBody] MedicalRecordCreateDTO medicalRecordDto, [FromQuery] int patientId)
    {
        try 
        {
            // Verificar si el modelo recibido es válido
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _medicalRecordService.CreateMedicalRecord(patientId, medicalRecordDto.CreatedAt, medicalRecordDto.DoctorName, medicalRecordDto.Treatment, medicalRecordDto.TreatmentCost,  medicalRecordDto.Notes);
            return Ok($"Se ha creado correctamente el historial médico para el paciente con Id: {patientId}");
        }     
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    //PUT: /MedicalRecord/{id}
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

    // DELETE: /MedicalRecord/{MedicalRecordId}
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
