using MongoDB.Driver;
using PrescriptionService.DTOs;
using PrescriptionService.Entities;

namespace PrescriptionService.Repositories
{
    public class PrescriptionRepository : IPrescriptionRepository
    {
        private readonly IMongoCollection<Prescription> _prescriptionCollection;

        public PrescriptionRepository(IMongoDatabase database)
        {
            _prescriptionCollection = database.GetCollection<Prescription>("prescriptions");
        }

        public async Task<List<Prescription>> GetPrescriptionsAsync(GetPrescriptionsDTO filter)
        {
            var filterBuilder = Builders<Prescription>.Filter;
            var filters = new List<FilterDefinition<Prescription>>();

            if (filter.DoctorId.HasValue)
                filters.Add(filterBuilder.Eq(p => p.DoctorId, filter.DoctorId.Value));
            if (!string.IsNullOrEmpty(filter.TCNumber))
                filters.Add(filterBuilder.Eq(p => p.TcNumber, filter.TCNumber));
            if (!string.IsNullOrEmpty(filter.Status))
                filters.Add(filterBuilder.Eq(p => p.Status, filter.Status));

            var combinedFilter = filters.Count > 0 ? filterBuilder.And(filters) : FilterDefinition<Prescription>.Empty;
            return await _prescriptionCollection.Find(combinedFilter).ToListAsync();
        }

        public async Task UpdatePrescriptionAsync(UpdatePrescriptionDTO dto)
        {
            var filter = Builders<Prescription>.Filter.Eq(p => p.PrescriptionId, dto.PrescriptionId);
            var update = Builders<Prescription>.Update.Set(p => p.Status, dto.Status);
            await _prescriptionCollection.UpdateOneAsync(filter, update);
        }

        public async Task CreatePrescriptionAsync(Prescription prescription)
        {
            await _prescriptionCollection.InsertOneAsync(prescription);
        }
    }
}
