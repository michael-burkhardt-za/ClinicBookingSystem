using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public interface IClinicService
    {
        Task<IEnumerable<Clinic>> GetAllAsync();

        Task<Clinic> AddAsync(Clinic clinic);

        Task<Clinic?> GetByIdAsync(int id);

        Task<bool> UpdateAsync(Clinic clinic);

        Task<bool> DeleteAsync(int id);
         
    }
}
