using DoctorService.Entities;
using MongoDB.Driver;

namespace DoctorService.Repositories
{
    public class VisitRepository : IVisitRepository
    {
        private readonly IMongoCollection<Visit> _visitsCollection;

        public VisitRepository(IMongoDatabase database)
        {
            _visitsCollection = database.GetCollection<Visit>("visits");
        }

        public async Task CreateAsync(Visit visit)
        {
            await _visitsCollection.InsertOneAsync(visit);
        }
    }
}