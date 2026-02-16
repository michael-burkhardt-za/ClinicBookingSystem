using Client.Dto;

namespace Client.Services.Interfases
{
    public interface IAppointmentApiService
    {
        Task<IEnumerable<DateTime>> GetAvailableSlots(int clinicId, DateTime date);
        Task<int> BookAppointment(AppointmentBookingDto appointment);
        Task <IEnumerable<AppointmentDto>> GetClinicAppointments(int clinicid);
        Task<IEnumerable<AppointmentDto>> GetPatientAppointments(int clinicid);
    }
}
