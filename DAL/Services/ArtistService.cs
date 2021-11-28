using ArtistPortfolio.DAL.Models;
using DAL.Repository;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Helpers;
using Microsoft.Extensions.Options;

namespace DAL.Services
{
    public class ArtistService
    {
        public readonly IMongoCollection<Artist> _collection;
        private readonly ArtistDatabaseSettings _settings;

        public ArtistService(IOptions<ArtistDatabaseSettings> settings)
        {
            _settings = settings.Value;
            var db = new MongoClient(_settings.ConnectionString).GetDatabase(_settings.DatabaseName);

            _collection = db.GetCollection<Artist>(DocumentHelper.GetCollectionName(typeof(Artist)));
        }

        public async Task<List<Artist>> GetAll()
        {
            return await _collection.Find(x=>true).ToListAsync();
        }
    }
}
