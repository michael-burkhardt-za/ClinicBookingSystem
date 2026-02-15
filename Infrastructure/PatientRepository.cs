using Application;
using Domain;
using Dapper;
using System.Data;
 

namespace Infrastructure
{
    public class PatientRepository : IPatientRepository
    {
        private readonly IDbConnection _db;
        public PatientRepository(IDbConnection db)
        {
            _db = db;
        }
        
        public async Task<IEnumerable<Patient>> GetAllAsync()
        {
            var sql = "SELECT * FROM Patients";
            return await _db.QueryAsync<Patient>(sql);
        }
        
        public async Task<Patient> AddAsync(Patient patient)
        {
            var sql = @"
                INSERT INTO Patients (FirstName, LastName, Email )
                VALUES (@FirstName, @LastName, @Email);
                SELECT CAST(SCOPE_IDENTITY() as int);";

            patient.Id = await _db.ExecuteScalarAsync<int>(sql, patient);
            return patient;
        }

        public async Task<Patient?> GetByIdAsync(int id)
        {
            var sql = "SELECT Id, FirstName, LastName, Email FROM Patients WHERE Id = @Id";
            return await _db.QueryFirstOrDefaultAsync<Patient>(sql, new { Id = id });
        }

        public async Task<bool> UpdateAsync(Patient patient)
        {
            if (patient.Id == 0) return false;

            var sql = @"
                        UPDATE Patients
                        SET FirstName = @FirstName,
                            LastName = @LastName,
                            Email = @Email
                        WHERE Id = @Id";

            var rowsAffected = await _db.ExecuteAsync(sql, patient);
            return rowsAffected > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var sql = "DELETE FROM Patients WHERE Id = @Id";

            var rowsAffected = await _db.ExecuteAsync(sql, new { Id = id });
            return rowsAffected > 0;
        }
    }
}
