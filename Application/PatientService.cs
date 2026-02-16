using Domain;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public class PatientService : IPatientService
    {
        private readonly IPatientRepository _repository;
        private readonly ILogger<PatientService> _logger;

        public PatientService(IPatientRepository repository, ILogger<PatientService> logger)
        {
            _repository = repository;
            _logger = logger;
        }
        public async Task<Patient> AddAsync(Patient patient)
        {
            return await _repository.AddAsync(patient);
        }
        public async Task<IEnumerable<Patient>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var ok = await _repository.DeleteAsync(id);
            return ok;
        }

        public async Task<Patient?> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<bool> UpdateAsync(Patient patient)
        {
            var ok = await _repository.UpdateAsync(patient);
            return ok;
        }
    }
}
