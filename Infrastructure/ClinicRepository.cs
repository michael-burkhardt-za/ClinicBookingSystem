using Application;
using Dapper;
using Domain;
using System.Data;
 

namespace Infrastructure
{
    public class ClinicRepository : IClinicRepository
    {
        private readonly IDbConnection _db;

        public ClinicRepository(IDbConnection db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Clinic>> GetAllAsync()
        {
            var sql = "SELECT * FROM Clinics";
            return await _db.QueryAsync<Clinic>(sql);
        }

        public async Task<Clinic> AddAsync(Clinic clinic)
        {
            var sql = @"
                INSERT INTO Clinics (Name, Phone, Address )
                VALUES (@Name, @Phone, @Address);
                SELECT CAST(SCOPE_IDENTITY() as int);";

            clinic.Id = await _db.ExecuteScalarAsync<int>(sql, clinic);
            return clinic;
        }
    }
}
