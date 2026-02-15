using Application;
using Dapper;
using Domain;
using System.Data;
 

namespace Infrastructure
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly IDbConnection _db;
        public AppointmentRepository(IDbConnection db)//(string connectionstring)
        {
            _db = db;
        }
        public async Task<IEnumerable<DateTime>> GetAvailableSlots(int clinicId, DateTime date)
        {

            var booked = await _db.QueryAsync<DateTime>(
                "SELECT AppointmentDate FROM Appointments WHERE ClinicId = @clinicId AND CAST(AppointmentDate AS DATE) = @date",
                new { clinicId, date });

            var allSlots = Enumerable.Range(9, 8)
                .Select(hour => date.Date.AddHours(hour));

            return allSlots.Except(booked);
        }

        public async Task<int> CreateAppointment(Appointment appointment)
        {
            var sql = @"INSERT INTO Appointments 
                    (ClinicId, PatientId, AppointmentDate, Status)
                    VALUES (@ClinicId, @PatientId, @AppointmentDate, @Status);
                    SELECT CAST(SCOPE_IDENTITY() as int)";

            return await _db.ExecuteScalarAsync<int>(sql, appointment);
        }

        //public async Task<IEnumerable<DateTime>> GetAvailableSlots(int clinicId, DateTime date)
        //{
        //    var sql = @"
        //    SELECT SlotTime
        //    FROM AppointmentSlots
        //    WHERE ClinicId = @ClinicId
        //      AND Date = @Date
        //      AND SlotTime NOT IN (
        //          SELECT Time FROM Appointments 
        //          WHERE ClinicId = @ClinicId AND Date = @Date
        //      );";

        //    return await _db.QueryAsync<DateTime>(sql, new { ClinicId = clinicId, Date = date });
        //}
    }
}
