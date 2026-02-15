using Application;
using Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ClinicsController : ControllerBase
    {
        private readonly IClinicService _clinicService;
        private readonly ILogger<ClinicsController> _logger;
        public ClinicsController(IClinicService clinicService, ILogger<ClinicsController> logger)
        { 
            _logger = logger;
            _clinicService = clinicService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            _logger.Log(LogLevel.Information, $"{nameof(GetAll)}");
            var clinics = await _clinicService.GetAllAsync();
            return Ok(clinics);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] Clinic clinic)
        {
            _logger.Log(LogLevel.Information, $"{nameof(Add)}");
            var created = await _clinicService.AddAsync(clinic);
            return CreatedAtAction(nameof(GetAll), new { id = created.Id }, created);
        }
    }
}
