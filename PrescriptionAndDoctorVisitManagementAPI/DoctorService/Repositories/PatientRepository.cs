using DoctorService.Entities;
using MongoDB.Driver;

namespace DoctorService.Repositories
{
    public class PatientRepository : IPatientRepository
    {
        private readonly IMongoCollection<Patient> _patients;

        public PatientRepository(IMongoDatabase database)
        {
            _patients = database.GetCollection<Patient>("patients");
        }

        public async Task<Patient> GetByTCNumberAsync(string tcNumber)
        {
            return await _patients.Find(patient => patient.TCNumber == tcNumber).FirstOrDefaultAsync();
        }

        public async Task CreateAsync(Patient patient)
        {
            await _patients.InsertOneAsync(patient);
        }
    }
}
