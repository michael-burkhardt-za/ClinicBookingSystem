using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public interface IAppointmentRepository
    {
        Task<IEnumerable<DateTime>> GetAvailableSlots(int clinicId, DateTime date);
        Task<int> CreateAppointment(AppointmentBooking appointment);
        Task<IEnumerable<Appointment>> GetClinicAppointments(int clinicId);
        Task<IEnumerable<Appointment>> GetPatientAppointments(int patientid);
        Task<bool> CheckPatientAlreadyBookedDate(AppointmentBooking appointment);
    }
}
