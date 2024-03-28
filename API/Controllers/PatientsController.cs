using Microsoft.AspNetCore.Mvc;
using CliniCareApp.Business;
using CliniCareApp.Models;
using Microsoft.AspNetCore.Authorization; 

namespace CliniCareApp.API.Controllers;

[ApiController]
[Route("[controller]")] 
[Authorize]
public class PatientsController : ControllerBase
{
    private readonly ILogger<PatientsController> _logger;
    private readonly IPatientService _patientService;
    private readonly PrivateAreaAccess _privateAreaAccess;

    public PatientsController(ILogger<PatientsController> logger, IPatientService PatientService, PrivateAreaAccess privateAreaAccess)
    {
        _logger = logger;
        _patientService = PatientService;
        _privateAreaAccess = privateAreaAccess;
    }
      
    [HttpGet(Name = "GetAllPatients")] 
    public ActionResult<IEnumerable<Patient>> SearchPatients(string? dni,string? name, string? lastName, bool orderByNameAsc)
    {
        var query = _patientService.GetAllPatients().AsQueryable();

        if (!string.IsNullOrWhiteSpace(dni))
        {
            query = query.Where(p => p.Dni.Contains(dni));
        }

        if (!string.IsNullOrWhiteSpace(name))
        {
            query = query.Where(p => p.Name.Contains(name));
        }

        if (!string.IsNullOrWhiteSpace(lastName))
        {
            query = query.Where(p => p.LastName.Contains(lastName));
        }

        if (orderByNameAsc)
        {
            query = query.OrderBy(p => p.Name);
        }
        else
        {
            query = query.OrderByDescending(p => p.Name);
        }

        var patients = query.ToList();

        if (patients.Count == 0)
        {
            return NotFound();
        }

        return patients;
    }

    // GET: /Patients/{id}
    // [HttpGet("{patientId}", Name = "GetPatientById")]
    // public IActionResult GetPatient(int patientId)
    // {
    //     try
    //     {
    //         var patient = _patientService.GetPatientById(patientId);
    //         return Ok(patient);
           
    //     }
    //     catch (KeyNotFoundException)
    //     {
    //         return NotFound($"No existe el paciente con el Id {patientId}");
    //     }
    // }

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
            return CreatedAtAction(nameof(SearchPatients), new { patientId = patient.Id }, patient);
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
