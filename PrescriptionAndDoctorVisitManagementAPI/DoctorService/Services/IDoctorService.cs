using DoctorService.DTOs;

namespace DoctorService.Services
{
    public interface IDoctorService
    {
        Task CreateDoctorAsync(CreateDoctorDTO dto);
        Task<string> LoginAsync(LoginDTO dto);
        Task CreateVisitAsync(CreateVisitDTO dto, Guid doctorId);
        Task CreatePrescriptionAsync(CreatePrescriptionDTO dto);
        Task<bool> DoctorExistsAsync(Guid doctorId);
    }
}
