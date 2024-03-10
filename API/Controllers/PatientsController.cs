using Microsoft.AspNetCore.Mvc;

using CliniCareApp.Data;
using CliniCareApp.Business;
using CliniCareApp.Models;

namespace CliniCareApp.API.Controllers;

[ApiController]
[Route("[controller]")] 
public class PatientsController : ControllerBase
{
    private readonly ILogger<PatientsController> _logger;
    private readonly IPatientService _patientService;

    public PatientsController(ILogger<PatientsController> logger, IPatientService PatientService)
    {
        _logger = logger;
        _patientService = PatientService;
    }

    // GET: /Patients
    [HttpGet(Name = "GetAllPatients")] 
    public ActionResult<IEnumerable<Patient>> GetPatients()
    {
        try 
        {
            var patients = _patientService.GetAllPatients();

            if(patients.Any())
            {
                return Ok(patients);
            }
            else
            {
                return NotFound("No existen pacientes para mostrar");
            }
        }     
        catch (Exception ex)
        {
            return BadRequest(ex);
        }      
    }

    // GET: /Patients/{id}
    [HttpGet("{patientId}", Name = "GetPatientById")]
    public IActionResult GetPatient(int patientId)
    {
        try
        {
            var patient = _patientService.GetPatientById(patientId);
            return Ok(patient);
           
        }
        catch (KeyNotFoundException)
        {
            return NotFound($"No existe el paciente con el Id {patientId}");
        }
    }

    [HttpPost]
    public IActionResult NewPatient([FromBody] PatientCreateDTO patientDto)
    {
        try 
        {
            // Verificar si el modelo recibido es válido
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var patient = _patientService.CreatePatient(patientDto.Name, patientDto.LastName, patientDto.Address, patientDto.Dni, patientDto.Phone);
            return CreatedAtAction(nameof(GetPatient), new { patientId = patient.Id }, patient);
        }     
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    //PUT: /Patients/{id}
    [HttpPut("{patientId}")]
    public IActionResult UpdatePatient(int patientId, [FromBody] PatientUpdateDTO patientDto)
    {
        if (!ModelState.IsValid)  {return BadRequest(ModelState); } 

        try
        {
            _patientService.UpdatePatientDetails(patientId, patientDto);
            return Ok($"El paciente con Id: {patientId} ha sido actualizado correctamente");
        }
        catch (KeyNotFoundException)
        {
           return NotFound();
        }
    }

    // DELETE: /Patient/{PatientId}
    [HttpDelete("{patientId}")]
    public IActionResult DeletePatient(int patientId)
    {
        try
        {
            _patientService.DeletePatient(patientId);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            _logger.LogInformation(ex.Message);
            return NotFound();
        }
    }
}
