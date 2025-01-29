using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace DoctorService.Entities
{
    public class Patient
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string TCNumber { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
