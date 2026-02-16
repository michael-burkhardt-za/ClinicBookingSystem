namespace Client.Dto
{
    public class AppointmentBookingDto
    {
        public int ClinicId { get; set; }
        public int PatientId { get; set; }
        public DateTime AppointmentDate { get; set; }
    }
}
