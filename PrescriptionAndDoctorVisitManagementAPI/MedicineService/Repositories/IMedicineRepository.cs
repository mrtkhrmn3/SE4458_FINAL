using MedicineService.Entities;

namespace MedicineService.Repositories
{
    public interface IMedicineRepository
    {
        Task<List<Medicine>> GetMedicinesAsync();
        Task AddOrUpdateMedicinesAsync(List<Medicine> medicines);
    }
}
