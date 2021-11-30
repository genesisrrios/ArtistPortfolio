using DAL.Models;
using DAL.Repository;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ArtistPortfolio.DAL.Models
{
    [BsonCollection("Gallery")]
    public class Picture : IDocument
    {

        [BsonElement("Id")]
        public ObjectId Id { get; set; }

        [BsonElement("ArtistId")]
        public string ArtistId { get; set; }
        [BsonRepresentation(BsonType.String)]
        public string Image { get; set; }

        public string Size { get; set; }


        public DateTime CreatedAt { get; set; }
    }
}