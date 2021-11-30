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
        private readonly IMongoCollection<Artist> _artistCollection;
        private readonly IMongoCollection<Picture> _galleryCollection;
        private readonly ArtistDatabaseSettings _settings;

        public ArtistService(IOptions<ArtistDatabaseSettings> settings)
        {
            _settings = settings.Value;
            var db = new MongoClient(_settings.ConnectionString).GetDatabase(_settings.DatabaseName);

            _artistCollection = db.GetCollection<Artist>(DocumentHelper.GetCollectionName(typeof(Artist)));
            _galleryCollection = db.GetCollection<Picture>(DocumentHelper.GetCollectionName(typeof(Picture)));

        }

        public async Task<List<Picture>> GetGallery(int page, int pageSize)
        {
            return await _galleryCollection.Find(x=>true).ToListAsync();
        }
    }
}
