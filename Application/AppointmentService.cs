using Domain;
using FluentValidation;
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
        private readonly IValidator<AppointmentBooking> _validator;

        public AppointmentService(
                    IAppointmentRepository repository, 
                    ILogger<AppointmentService> logger, 
                    IValidator<AppointmentBooking> validator)
        {
            _repository = repository;
            _logger = logger;
            _validator = validator;

        }

        public async Task<IEnumerable<DateTime>> GetAvailableSlots(int clinicId, DateTime date)
        {
            if (clinicId <= 0)
                throw new ValidationException("ClinicId must be greater than zero.");
            
            var selectedDate = date;
            if (selectedDate < DateTime.UtcNow.Date)
                throw new ValidationException("Date cannot be in the past.");

            return await _repository.GetAvailableSlots(clinicId, date);
            
        }

        public async Task<int> BookAppointment(AppointmentBooking appointment)
        {
            appointment.Status = "Confirmed";

            var result = await _validator.ValidateAsync(appointment);
            if (!result.IsValid)
                throw new ValidationException(result.Errors);

            bool bookingdateusedbyuser = await _repository.CheckPatientAlreadyBookedDate(appointment);
            if(bookingdateusedbyuser)
                throw new ValidationException("Patient aready has an appointment on this date");

            return await _repository.CreateAppointment(appointment);
        }

        public async Task<IEnumerable<Appointment>> GetClinicAppointments(int clinicId)
        {
            if (clinicId <= 0)
                throw new ValidationException("ClinicId must be greater than zero.");

            return await _repository.GetClinicAppointments(clinicId);
        }
         
        public async Task<IEnumerable<Appointment>> GetPatientsAppointments(int patientid)
        {
            if (patientid <= 0)
                throw new ValidationException("PatientId must be greater than zero.");

            return await _repository.GetPatientAppointments(patientid);
        }
    }
}
