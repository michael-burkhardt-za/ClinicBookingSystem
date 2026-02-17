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
        Task<int> BookAppointment(AppointmentBooking appointment);
        Task<IEnumerable<Appointment>> GetClinicAppointments(int clinicId);
        Task<IEnumerable<Appointment>> GetPatientsAppointments(int patientid);
        Task<bool> DeleteAsync(int appointmentid);
    }
}
