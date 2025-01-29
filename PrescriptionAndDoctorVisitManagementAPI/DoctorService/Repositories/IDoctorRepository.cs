using DoctorService.DTOs;
using DoctorService.Entities;

namespace DoctorService.Repositories
{
    public interface IDoctorRepository
    {
        Task<Doctor> GetByUsernameAsync(string username);
        Task CreateAsync(Doctor doctor);
        Task CreatePrescriptionAsync(Prescription prescription);
        Task<Doctor> GetByIdAsync(Guid id);
    }
}
