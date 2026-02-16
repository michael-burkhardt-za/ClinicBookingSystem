using Application;
using Domain;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public class ClinicService : IClinicService
    {
        private readonly IClinicRepository _repository;
        private readonly ILogger<ClinicService> _logger;

        public ClinicService(IClinicRepository repository, ILogger<ClinicService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<IEnumerable<Clinic>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Clinic> AddAsync(Clinic clinic)
        {
            return await _repository.AddAsync(clinic);
        }

        public async Task<Clinic?> GetByIdAsync(int id)
        {
           return await _repository.GetByIdAsync(id);
        }

        public async Task<bool> UpdateAsync(Clinic clinic)
        {
             var ok = await _repository.UpdateAsync(clinic);
             return ok;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var ok = await _repository.DeleteAsync(id);
            return ok;
        }
    }
}



