using Client.Dto;

namespace Client.Services.Interfases
{
    public interface IPatientApiService
    {
        Task<IEnumerable<PatientDto>> GetPatients();
        Task<PatientDto?> GetPatient(int? patientid);
        Task<PatientDto?> AddPatient(PatientDto patient);
        Task<HttpResponseMessage> UpdatePatient(PatientDto patient);
        Task<HttpResponseMessage> DeletePatient(int patientid);
    }
}
