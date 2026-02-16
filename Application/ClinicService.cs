using Domain;
using FluentValidation;
using Microsoft.Extensions.Logging;
 

namespace Application
{
    public class ClinicService : IClinicService
    {
        private readonly IClinicRepository _repository;
        private readonly ILogger<ClinicService> _logger;
        private readonly IValidator<Clinic> _validator;
        

        public ClinicService(IClinicRepository repository,
                        ILogger<ClinicService> logger, 
                        IValidator<Clinic> validator)
        {
            _repository = repository;
            _logger = logger;
            _validator = validator;
            
        }

        public async Task<IEnumerable<Clinic>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Clinic> AddAsync(Clinic clinic)
        {
            var result = await _validator.ValidateAsync(clinic);

            if (!result.IsValid)
                throw new ValidationException(result.Errors);

            return await _repository.AddAsync(clinic);
        }

        public async Task<Clinic?> GetByIdAsync(int id)
        {
            var clinic = await _repository.GetByIdAsync(id);
            if (clinic == null)
                throw new KeyNotFoundException($"Clinic with id {id} not found.");

            return clinic;
        }

        public async Task<bool> UpdateAsync(Clinic clinic)
        {

            var ok = await _repository.UpdateAsync(clinic);
            if (!ok)
                throw new KeyNotFoundException($"Clinic with id {clinic.Id} not found.");
            
            return ok;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var ok = await _repository.DeleteAsync(id);
            if (!ok)
                throw new KeyNotFoundException($"Clinic with id {id} not found.");
            return ok;
        }
    }
}



