using Application;
using Dapper;
using Domain;
using System.Data;
 

namespace Infrastructure
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly IDbConnection _db;
        public AppointmentRepository(IDbConnection db)
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
        public async Task<int> CreateAppointment(AppointmentBooking appointment)
        {
            var sql = @"INSERT INTO Appointments 
                    (ClinicId, PatientId, AppointmentDate, Status)
                    VALUES (@ClinicId, @PatientId, @AppointmentDate, @Status);
                    SELECT CAST(SCOPE_IDENTITY() as int)";

            return await _db.ExecuteScalarAsync<int>(sql, appointment);
        }
        public async Task<IEnumerable<Appointment>> GetClinicAppointments(int clinicid)
        {

            var sql = @"
            SELECT
                a.Id, 
                a.ClinicId, 
                a.PatientId,
                a.AppointmentDate,
                a.Status, 
                c.Name as ClinicName, 
                p.id as PatientId, 
                p.FirstName, 
                p.LastName, 
                p.Email
              FROM
              Appointments a
              left join Clinics c on a.ClinicId = c.Id 
              left join Patients p on p.Id = a.PatientId
 
              where c.Id = @ClinicId
              order by  c.Id , a.AppointmentDate 
            ";
            return await _db.QueryAsync<Appointment>(sql, new { ClinicId = clinicid });

             
        }
        public async Task<IEnumerable<Appointment>> GetPatientAppointments(int patientid)
        {
            var sql = @"
            SELECT
                a.Id, 
                a.ClinicId, 
                a.PatientId,
                a.AppointmentDate,
                a.Status, 
                c.Name as ClinicName, 
                p.id as PatientId, 
                p.FirstName, 
                p.LastName, 
                p.Email
            FROM
                Appointments a
                left join Clinics c on a.ClinicId = c.Id 
                left join Patients p on p.Id = a.PatientId
 
            WHERE p.Id = @PatientId
            ORDER BY c.Id , a.AppointmentDate 
            ";
            return await _db.QueryAsync<Appointment>(sql, new { PatientId = patientid });
        }
        public async Task<bool> CheckPatientAlreadyBookedDate(AppointmentBooking appointment)
        {
            var sql = @"
                SELECT COUNT(1)
                FROM Appointments
                WHERE PatientId = @PatientId
                AND AppointmentDate = @AppointmentDate;
                ";

            var count = await _db.ExecuteScalarAsync<int>(
                sql,new { appointment.PatientId, appointment.AppointmentDate });

            return count > 0;

           
        }
    }
}
