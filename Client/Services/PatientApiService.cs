using Client.Dto;
using Client.Services.Interfases;
using System.Net.Http.Json;


namespace Client.Services
{
    public class PatientApiService : IPatientApiService
    {
        private readonly HttpClient _http;
        private readonly ILogger<PatientApiService> _logger;
        public PatientApiService(HttpClient http, ILogger<PatientApiService> logger)
        {
            _http = http;
            _logger = logger;

        }
        public async Task<IEnumerable<PatientDto>> GetPatients()
        {
            return await _http.GetFromJsonAsync<IEnumerable<PatientDto>>("patients")
                   ?? Enumerable.Empty<PatientDto>();
        }
        public async Task<PatientDto?> GetPatient(int? patientid)
        {
            if (patientid == null) return null;
            return await _http.GetFromJsonAsync<PatientDto>($"patients/{patientid}");
        }
        public async Task<PatientDto?> AddPatient(PatientDto patient)
        {
            var response = await _http.PostAsJsonAsync("patients", patient);
            return await response.Content.ReadFromJsonAsync<PatientDto>()!;
        }
        public async Task<HttpResponseMessage> UpdatePatient(PatientDto patient)
        {
            var response = await _http.PutAsJsonAsync("patients", patient);
            return response;
        }
        public async Task<HttpResponseMessage> DeletePatient(int patientid)
        {
            var response = await _http.DeleteAsync($"patients/{patientid}");
            return response;
        }
    }
}
