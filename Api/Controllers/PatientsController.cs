using Application;
using Domain;
using Microsoft.AspNetCore.Http;
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
            var clinics = await _patientService.GetAllAsync();
            return Ok(clinics);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] Patient patient)
        {
            _logger.Log(LogLevel.Information, $"{nameof(Add)}");
            var created = await _patientService.AddAsync(patient);
            return CreatedAtAction(nameof(GetAll), new { id = created.Id }, created);
        }
    }
}
