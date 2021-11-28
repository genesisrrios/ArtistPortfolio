using DAL.Models;
using DAL.Repository;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ArtistPortfolio.DAL.Models
{
    [BsonCollection("Artist")]
    public class Artist : IDocument
    {

        [BsonElement("Name")]
        public string Name { get; set; }

        [BsonElement("Bio")]
        public string Bio { get; set; }
        [BsonRepresentation(BsonType.Binary)]
        public byte[] Picture { get; set; }

        public DateTime CreatedAt { get; set; }

        public ObjectId Id { get; set; }
    }
}