using PrescriptionService.DTOs;
using PrescriptionService.Entities;

namespace PrescriptionService.Repositories
{
    public interface IPrescriptionRepository
    {
        Task<List<Prescription>> GetPrescriptionsAsync(GetPrescriptionsDTO filter);
        Task UpdatePrescriptionAsync(UpdatePrescriptionDTO dto);
        Task CreatePrescriptionAsync(Prescription prescription);
    }
}
