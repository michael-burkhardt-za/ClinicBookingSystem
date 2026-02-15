using Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public class ClinicService : IClinicService
    {
        private readonly IClinicRepository _repository;

        public ClinicService(IClinicRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Clinic>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Clinic> AddAsync(Clinic clinic)
        {
            return await _repository.AddAsync(clinic);
        }
    }
}
