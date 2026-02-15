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

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            _logger.Log(LogLevel.Information, $"{nameof(Get)}");
            var clinic = await _clinicService.GetByIdAsync(id);
            if (clinic == null)
                return NotFound();

            return Ok(clinic);
        }
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] Clinic clinic)
        {
            _logger.Log(LogLevel.Information, $"{nameof(Add)}");
            var created = await _clinicService.AddAsync(clinic);
            return CreatedAtAction(nameof(GetAll), new { id = created.Id }, created);
        }
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Clinic clinic)
        {
            _logger.Log(LogLevel.Information, $"{nameof(Update)}");
            
            if (clinic.Id == 0)
                return BadRequest();

            var updated = await _clinicService.UpdateAsync(clinic);
            return Ok(updated);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            _logger.Log(LogLevel.Information, $"{nameof(Delete)}");
            var deleted = await _clinicService.DeleteAsync(id);
            return Ok(deleted);
        }
    }
}
