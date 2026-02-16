using Client.Dto;
using Client.Services.Interfases;
using System.Net.Http.Json;
using static System.Net.WebRequestMethods;

namespace Client.Services
{
    public class ClinicApiService : IClinicApiService
    {
        private readonly HttpClient _http;
        private readonly ILogger<ClinicApiService> _logger;

        public ClinicApiService(HttpClient http, ILogger<ClinicApiService> logger)
        {
            _http = http;
            _logger = logger;
        }

        public async Task<IEnumerable<ClinicDto>> GetClinics()
        {
            return await _http.GetFromJsonAsync<IEnumerable<ClinicDto>>("clinics")
                   ?? Enumerable.Empty<ClinicDto>();
        }
        public async Task<ClinicDto?> GetClinic(int? id)
        {
            if (id == null) return null;
            return await _http.GetFromJsonAsync<ClinicDto>($"clinics/{id}");
        }
        public async Task<ClinicDto?> AddClinic(ClinicDto clinic)
        {
            var response = await _http.PostAsJsonAsync("clinics", clinic);
            return await response.Content.ReadFromJsonAsync<ClinicDto>()!;
        }
        public async Task<HttpResponseMessage> DeleteClinic(int id)
        {
            var response = await _http.DeleteAsync($"clinics/{id}");
            return response;
        }
        public async Task<HttpResponseMessage> UpdateClinic(ClinicDto clinic)
        {
            var response = await _http.PutAsJsonAsync("clinics", clinic);
            return response;
        }
    }
}
