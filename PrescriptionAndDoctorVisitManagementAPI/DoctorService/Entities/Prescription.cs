using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace DoctorService.Entities
{
    public class Prescription
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonRepresentation(BsonType.String)]
        public Guid PrescriptionId { get; set; }

        [BsonRepresentation(BsonType.String)]
        public Guid DoctorId { get; set; }
        public string TCNumber { get; set; }
        public List<Medicine> Medicines { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; } 
    }
}
