using Domain;
using FluentValidation;
using Microsoft.Extensions.Logging;
 

namespace Application
{
    public class PatientService : IPatientService
    {
        private readonly IPatientRepository _repository;
        private readonly ILogger<PatientService> _logger;
        private readonly IValidator<Patient> _validator;
        

        public PatientService(IPatientRepository repository, 
                        ILogger<PatientService> logger,
                        IValidator<Patient> validator)
        {
            _repository = repository;
            _logger = logger;
            _validator = validator;
             
        }
        public async Task<Patient> AddAsync(Patient patient)
        {
            var result = await _validator.ValidateAsync(patient);
            if (!result.IsValid)
                throw new ValidationException(result.Errors);

            return await _repository.AddAsync(patient);
        }
        public async Task<IEnumerable<Patient>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var ok = await _repository.DeleteAsync(id);
            if (!ok)
                throw new KeyNotFoundException($"Patient with id {id} not found.");
            return ok;
        }

        public async Task<Patient?> GetByIdAsync(int id)
        {
             var patient = await _repository.GetByIdAsync(id);

            if (patient == null)
                throw new KeyNotFoundException($"Patient with id {id} not found.");

            return patient;
        }

        public async Task<bool> UpdateAsync(Patient patient)
        {
            var ok = await _repository.UpdateAsync(patient);
            if (!ok)
                throw new KeyNotFoundException($"Patient with id {patient.Id} not found.");
            return ok;
        }
    }
}
