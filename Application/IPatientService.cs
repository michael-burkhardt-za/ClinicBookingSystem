using Domain;

namespace Application
{
    public interface IPatientService
    {
        Task<Patient> AddAsync(Patient patient);
        Task<IEnumerable<Patient>> GetAllAsync();
    }
}