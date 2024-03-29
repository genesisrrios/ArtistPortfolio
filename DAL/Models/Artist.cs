﻿using DAL.Repository;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DAL.Models
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
        public string UserId { get; set; }
        public string Password { get; set; }
    }
}