using Client.Dto;

namespace Client.Services.Interfases
{
    public interface IClinicApiService
    {
        Task<ClinicDto?> AddClinic(ClinicDto clinic);
        Task<HttpResponseMessage> DeleteClinic(int id);
        Task<ClinicDto?> GetClinic(int? id);
        Task<IEnumerable<ClinicDto>> GetClinics();
        Task<HttpResponseMessage> UpdateClinic(ClinicDto clinic);
    }
}