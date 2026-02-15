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
    }
}
