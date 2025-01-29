using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace PharmacyService.Entities
{
    public class Pharmacy
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonRepresentation(BsonType.String)]
        public Guid PharmacyId { get; set; } 
        public string Name { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
