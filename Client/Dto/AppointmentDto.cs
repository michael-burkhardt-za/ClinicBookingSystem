namespace Client.Dto
{
    public class AppointmentDto
    {
        public int Id { get; set; }

        public int ClinicId { get; set; }

        public int PatientId { get; set; }

        public string ClinicName { get; set; } = string.Empty;

        public DateTime AppointmentDate { get; set; }

        public string Status { get; set; } = string.Empty;

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;
    }

}
