using Client.Dto;
using Client.Services.Interfases;
using System.Net.Http.Json;
 

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
            var response = await _http.GetAsync(
                       $"appointments/available?clinicId={clinicId}&date={date:yyyy-MM-dd}");

            return await response.Content.ReadFromJsonAsync<IEnumerable<DateTime>>()
                   ?? Enumerable.Empty<DateTime>();
            
        }

        public async Task<int> BookAppointment(AppointmentBookingDto appointment)
        {
            var response = await _http.PostAsJsonAsync("appointments", appointment);

            if (!response.IsSuccessStatusCode) return 0;

            return await response.Content.ReadFromJsonAsync<int>();
            
        }

        public async Task<IEnumerable<AppointmentDto>> GetClinicAppointments(int clinicId)
        {
            return await _http.GetFromJsonAsync<IEnumerable<AppointmentDto>>($"appointments/clinics/{clinicId}")
                   ?? Enumerable.Empty<AppointmentDto>();
        }

        public async Task<IEnumerable<AppointmentDto>> GetPatientAppointments(int patientId)
        {
            return await _http.GetFromJsonAsync<IEnumerable<AppointmentDto>>($"appointments/patient/{patientId}")
                  ?? Enumerable.Empty<AppointmentDto>();
        }

        public async Task<HttpResponseMessage> DeleteAppointment(int id)
        {
            var response = await _http.DeleteAsync($"appointments/{id}");
            return response;
        }
    }
}
