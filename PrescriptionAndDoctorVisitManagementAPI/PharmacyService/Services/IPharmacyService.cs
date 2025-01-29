using PharmacyService.DTOs;
using PrescriptionService.Entities;

namespace PharmacyService.Services
{
    public interface IPharmacyService
    {
        Task CreatePharmacyAsync(CreatePharmacyDTO dto);
        Task<string> AuthenticateAsync(LoginPharmacyDTO dto);
        Task UpdatePrescriptionAsync(HandlePrescriptionDTO dto);
        Task<List<Prescription>> GetPrescriptionsAsync(Guid pharmacyId);
        Task HandlePrescriptionAsync(HandlePrescriptionDTO dto);
    }
}
