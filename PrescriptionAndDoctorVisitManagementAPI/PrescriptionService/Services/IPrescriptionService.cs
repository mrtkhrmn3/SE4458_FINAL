using PrescriptionService.DTOs;
using PrescriptionService.Entities;

namespace PrescriptionService.Services
{
    public interface IPrescriptionService
    {
        Task<List<Prescription>> GetPrescriptionsAsync(GetPrescriptionsDTO filter);
        Task UpdatePrescriptionAsync(UpdatePrescriptionDTO dto);
        Task QueuePrescriptionAsync(Prescription prescription);
        Task CheckMissingMedicines(Guid prescriptionId);
    }
}
