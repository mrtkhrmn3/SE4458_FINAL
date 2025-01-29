using DoctorService.DTOs;
using DoctorService.Entities;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DoctorService.Repositories
{
    public class DoctorRepository : IDoctorRepository
    {
        private readonly IMongoCollection<Doctor> _doctors;
        private readonly IMongoCollection<Prescription> _prescriptions;

        public DoctorRepository(IMongoDatabase database)
        {
            _doctors = database.GetCollection<Doctor>("doctors");
            _prescriptions = database.GetCollection<Prescription>("prescriptions");
        }

        public async Task<Doctor> GetByUsernameAsync(string username)
        {
            return await _doctors.Find(doctor => doctor.Username == username).FirstOrDefaultAsync();
        }

        public async Task CreateAsync(Doctor doctor)
        {
            await _doctors.InsertOneAsync(doctor);
        }

        public async Task CreatePrescriptionAsync(Prescription prescription)
        {
            if (prescription == null)
            {
                throw new ArgumentNullException(nameof(prescription), "Prescription cannot be null.");
            }

            await _prescriptions.InsertOneAsync(prescription);
        }

        public async Task<Doctor> GetByIdAsync(Guid doctorId)
        {
            return await _doctors.Find(d => d.DoctorId == doctorId).FirstOrDefaultAsync();
        }
    }
}
