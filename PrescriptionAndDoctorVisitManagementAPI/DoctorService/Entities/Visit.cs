using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace DoctorService.Entities
{
    public class Visit
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonRepresentation(BsonType.String)]
        public Guid VisitId { get; set; } 

        [BsonRepresentation(BsonType.String)]
        public Guid DoctorId { get; set; } 
        public string TCNumber { get; set; } 
        public DateTime VisitDate { get; set; } 
        public string Notes { get; set; } 
    }
}
