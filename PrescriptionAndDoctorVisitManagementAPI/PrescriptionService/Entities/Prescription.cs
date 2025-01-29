using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace PrescriptionService.Entities
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
        public string TcNumber { get; set; }
        public List<Medicine> Medicines { get; set; }
        public string Status { get; set; } // "completed", "incomplete"
        public DateTime CreatedAt { get; set; }
    }

    public class Medicine
    {
        public string Name { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
