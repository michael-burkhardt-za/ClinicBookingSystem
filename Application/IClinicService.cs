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
    }
}
