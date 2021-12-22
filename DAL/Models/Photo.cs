using DAL.Models;
using DAL.Repository;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DAL.Models
{
    [BsonCollection("Gallery")]
    public class Picture : IDocument
    {
        [BsonId]
        [BsonElement("Id")]
        public ObjectId Id { get; set; }

        [BsonElement("ArtistId")]
        public string ArtistId { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }

        public string[] Properties { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}