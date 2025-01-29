using PharmacyService.DTOs;
using PharmacyService.Entities;

namespace PharmacyService.Repositories
{
    public interface IPharmacyRepository
    {
        Task CreateAsync(Pharmacy pharmacy);
        Task<Pharmacy> GetByUsernameAsync(string username);
        Task UpdatePrescriptionAsync(HandlePrescriptionDTO dto);
    }
}
