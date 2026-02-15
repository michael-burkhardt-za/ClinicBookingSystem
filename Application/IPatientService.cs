using Domain;

namespace Application
{
    public interface IPatientService
    { 
        Task<IEnumerable<Patient>> GetAllAsync();
        Task<Patient> AddAsync(Patient patient);

        Task<Patient?> GetByIdAsync(int id);

        Task<bool> UpdateAsync(Patient patient);

        Task<bool> DeleteAsync(int id);

    }
}