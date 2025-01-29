using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace DoctorService.Entities
{
    public class Doctor
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonRepresentation(BsonType.String)]
        public Guid DoctorId { get; set; }
        public string Name { get; set; }
        public string Specialization { get; set; }
        public string PasswordHash { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Username { get; set; }
    }
}
