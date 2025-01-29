using MongoDB.Driver;
using PharmacyService.DTOs;
using PharmacyService.Entities;
using PrescriptionService.Entities;

namespace PharmacyService.Repositories
{
    public class PharmacyRepository : IPharmacyRepository
    {
        private readonly IMongoCollection<Pharmacy> _pharmacies;
        private readonly IMongoCollection<Prescription> _prescriptions;

        public PharmacyRepository(IMongoDatabase database)
        {
            _pharmacies = database.GetCollection<Pharmacy>("pharmacies");
            _prescriptions = database.GetCollection<Prescription>("prescriptions");
        }

        public async Task CreateAsync(Pharmacy pharmacy)
        {
            await _pharmacies.InsertOneAsync(pharmacy);
        }

        public async Task<Pharmacy> GetByUsernameAsync(string username)
        {
            var filter = Builders<Pharmacy>.Filter.Eq(p => p.Username, username);
            return await _pharmacies.Find(filter).FirstOrDefaultAsync();
        }

        public async Task UpdatePrescriptionAsync(HandlePrescriptionDTO dto)
        {
            var filter = Builders<Prescription>.Filter.Eq(p => p.PrescriptionId, dto.PrescriptionId);
            var update = Builders<Prescription>.Update
                .Set(p => p.Status, "Updated")
                .Set("MissingMedicines", dto.MissingMedicines);

            await _prescriptions.UpdateOneAsync(filter, update);
        }
    }
}
