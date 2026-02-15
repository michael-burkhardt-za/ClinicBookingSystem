using Client.Dto;
using System.Net.Http.Json;

namespace Client.Services
{
    public class ClinicApiService
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

        public async Task<ClinicDto> AddClinic(ClinicDto clinic)
        {
            var response = await _http.PostAsJsonAsync("clinics", clinic);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<ClinicDto>()!;
        }
    }
}
