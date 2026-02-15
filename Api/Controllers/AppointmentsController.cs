using Application;
using Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class AppointmentsController : ControllerBase
    {
        private readonly IAppointmentService _service;
        private readonly ILogger<AppointmentsController> _logger;
        public AppointmentsController(IAppointmentService service, ILogger<AppointmentsController> logger)
        {
            _logger = logger;
            _service = service;
        }

        [HttpGet("available")]
        public async Task<IActionResult> GetAvailable(int clinicId, DateTime date)
        {
            _logger.Log(LogLevel.Information, $"{nameof(GetAvailable)}");
            var slots = await _service.GetAvailableSlots(clinicId, date);
            return Ok(slots);
        }

        [HttpPost]
        public async Task<IActionResult> Book(Appointment appointment)
        {
            _logger.Log(LogLevel.Information, $"{nameof(Book)}");
            var id = await _service.BookAppointment(appointment);
            return Ok(id);
        }
    }
}
