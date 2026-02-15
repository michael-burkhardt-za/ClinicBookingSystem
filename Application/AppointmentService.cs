using Domain;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger<AppointmentService> _logger;

        public AppointmentService(IAppointmentRepository repository, ILogger<AppointmentService> logger)
        {
            _repository = repository;
            _logger = logger;

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
