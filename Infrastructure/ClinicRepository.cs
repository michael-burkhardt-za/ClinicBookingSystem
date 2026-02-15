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

        public async Task<Clinic?> GetByIdAsync(int id)
        {
            var sql = "SELECT Id, Name, Address, Phone FROM Clinics WHERE Id = @Id";
            return await _db.QueryFirstOrDefaultAsync<Clinic>(sql, new { Id = id });
        }

        public async Task<bool> UpdateAsync(Clinic clinic)
        {
            if (clinic.Id == 0) return false;

            var sql = @"
                        UPDATE Clinics
                        SET Name = @Name,
                            Address = @Address,
                            Phone = @Phone
                        WHERE Id = @Id";

            var rowsAffected = await _db.ExecuteAsync(sql, clinic);
            return rowsAffected > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var sql = "DELETE FROM Clinics WHERE Id = @Id";

            var rowsAffected = await _db.ExecuteAsync(sql, new { Id = id });
            return rowsAffected > 0;
        }

         
    }
}




