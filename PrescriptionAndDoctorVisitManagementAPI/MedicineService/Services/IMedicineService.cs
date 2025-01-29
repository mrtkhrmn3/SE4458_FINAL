using MedicineService.DTOs;
using MedicineService.Entities;

namespace MedicineService.Services
{
    public interface IMedicineService
    {
        Task<List<Medicine>> SearchMedicinesAsync(string query);
        Task UpdateMedicinesAsync(List<UpdateMedicineDTO> medicines);
    }
}
