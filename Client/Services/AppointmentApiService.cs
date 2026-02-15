using Client.Dto;
using Client.Services.Interfases;
using Domain;
using System.Net.Http.Json;
using static Client.Pages.Weather;
using static System.Net.WebRequestMethods;

namespace Client.Services
{


    public class AppointmentApiService : IAppointmentApiService
    {
        private readonly HttpClient _http;
        private readonly ILogger<AppointmentApiService> _logger;

        public AppointmentApiService(HttpClient http, ILogger<AppointmentApiService> logger)
        {
            _http = http;
            _logger = logger;
        }

        public async Task<IEnumerable<DateTime>> GetAvailableSlots(int clinicId, DateTime date)
        {
            try
            {
                var response = await _http.GetAsync(
                    $"appointments/available?clinicId={clinicId}&date={date:yyyy-MM-dd}");

                response.EnsureSuccessStatusCode();

                return await response.Content.ReadFromJsonAsync<IEnumerable<DateTime>>()
                       ?? Enumerable.Empty<DateTime>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching available slots");
                throw;
            }
        }

        public async Task<int> BookAppointment(AppointmentDto appointment)
        {
            try
            {
                var response = await _http.PostAsJsonAsync(
                    "appointments",
                    appointment);

                response.EnsureSuccessStatusCode();

                return await response.Content.ReadFromJsonAsync<int>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error booking appointment");
                throw;
            }
        }
    }
}
