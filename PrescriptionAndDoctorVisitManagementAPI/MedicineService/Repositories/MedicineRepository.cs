using MedicineService.Entities;
using MongoDB.Driver;

namespace MedicineService.Repositories
{
    public class MedicineRepository : IMedicineRepository
    {
        private readonly IMongoCollection<Medicine> _medicines;

        public MedicineRepository(IMongoDatabase database)
        {
            _medicines = database.GetCollection<Medicine>("medicines");
        }

        public async Task<List<Medicine>> GetMedicinesAsync()
        {
            return await _medicines.Find(_ => true).ToListAsync();
        }

        public async Task AddOrUpdateMedicinesAsync(List<Medicine> medicines)
        {
            foreach (var medicine in medicines)
            {
                var filter = Builders<Medicine>.Filter.Eq(m => m.Name, medicine.Name);
                var update = Builders<Medicine>.Update
                    .Set(m => m.Price, medicine.Price)
                    .Set(m => m.UpdatedAt, DateTime.UtcNow);

                await _medicines.UpdateOneAsync(filter, update, new UpdateOptions { IsUpsert = true });
            }
        }
    }
}
