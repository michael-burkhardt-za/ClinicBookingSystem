using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public interface IAppointmentService
    {
        Task<IEnumerable<DateTime>> GetAvailableSlots(int clinicId, DateTime date);
        Task<int> BookAppointment(Appointment appointment);
    }
}
