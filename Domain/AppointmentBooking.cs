using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class AppointmentBooking
    {
        public int Id { get; set; }
        public int ClinicId { get; set; }
        public int PatientId { get; set; }
        public DateTime? AppointmentDate { get; set; }
        public string? Status { get; set; } = string.Empty;
    }
}
