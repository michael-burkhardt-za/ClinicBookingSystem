using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IAppointmentRepository _repository;

        public AppointmentService(IAppointmentRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<DateTime>> GetAvailableSlots(int clinicId, DateTime date)
        {
            return await _repository.GetAvailableSlots(clinicId, date);
        }

        public async Task<int> BookAppointment(Appointment appointment)
        {
            appointment.Status = "Confirmed";
            return await _repository.CreateAppointment(appointment);
        }
    }
}
