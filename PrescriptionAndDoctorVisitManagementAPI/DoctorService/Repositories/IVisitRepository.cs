using DoctorService.Entities;

namespace DoctorService.Repositories
{
    public interface IVisitRepository
    {
        Task CreateAsync(Visit visit);
    }
}
