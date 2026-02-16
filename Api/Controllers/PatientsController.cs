using Application;
using Domain;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
        private readonly IPatientService _patientService;
        private readonly ILogger<PatientsController> _logger;
        public PatientsController(IPatientService patientservice, ILogger<PatientsController> logger)
        {
            _logger = logger;
            _patientService = patientservice;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            _logger.Log(LogLevel.Information, $"{nameof(GetAll)}");
            var patients = await _patientService.GetAllAsync();
            return Ok(patients);
        }
        
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            _logger.Log(LogLevel.Information, $"{nameof(Get)}");
            var patient = await _patientService.GetByIdAsync(id);
            if (patient == null)
                return NotFound();

            return Ok(patient);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] Patient patient)
        {
            _logger.Log(LogLevel.Information, $"{nameof(Add)}");
            var created = await _patientService.AddAsync(patient);
            return CreatedAtAction(nameof(GetAll), new { id = created.Id }, created);
        }
        
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Patient patient)
        {
            _logger.Log(LogLevel.Information, $"{nameof(Update)}");

            if (patient.Id == 0)
                return BadRequest();

            var updated = await _patientService.UpdateAsync(patient);
            if (!updated)
                return NotFound();
            return Ok(updated);
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            _logger.Log(LogLevel.Information, $"{nameof(Delete)}");
            var deleted = await _patientService.DeleteAsync(id);
            if (!deleted)
                return NotFound();
            return Ok(deleted);
        }
    }
}
