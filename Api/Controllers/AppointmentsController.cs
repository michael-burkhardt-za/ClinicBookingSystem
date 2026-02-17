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
        public async Task<IActionResult> Book(AppointmentBooking appointment)
        {
            _logger.Log(LogLevel.Information, $"{nameof(Book)}");
            var id = await _service.BookAppointment(appointment);
            return Ok(id);
        }

        [HttpGet("clinics/{id:int}")]
        public async Task<IActionResult> GetClinicAppointments(int id)
        {
            _logger.Log(LogLevel.Information, $"{nameof(GetClinicAppointments)}");
            var appointments = await _service.GetClinicAppointments(id);
            return Ok(appointments);
        }

        [HttpGet("patient/{id:int}")]
        public async Task<IActionResult> GetPatientAppointments(int id)
        {
            _logger.Log(LogLevel.Information, $"{nameof(GetPatientAppointments)}");
            var appointments = await _service.GetPatientsAppointments(id);
            return Ok(appointments);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            _logger.Log(LogLevel.Information, $"{nameof(Delete)}");
            var deleted = await _service.DeleteAsync(id);
            return Ok(deleted);
        }
    }
}
