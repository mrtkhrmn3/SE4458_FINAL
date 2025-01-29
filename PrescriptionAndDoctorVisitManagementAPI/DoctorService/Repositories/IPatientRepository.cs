using DoctorService.Entities;

namespace DoctorService.Repositories
{
    public interface IPatientRepository
    {
        Task<Patient> GetByTCNumberAsync(string tcNumber);
        Task CreateAsync(Patient patient);
    }
}
