using DAL.Models;
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
        private readonly IMongoRepository<Artist> _artistCollection;
        private readonly IMongoRepository<Picture> _galleryCollection;
        private readonly ArtistDatabaseSettings _settings;

        public ArtistService(IOptions<ArtistDatabaseSettings> settings, IMongoRepository<Picture> pictureCollection, IMongoRepository<Artist> artistCollection)
        {
            _settings = settings.Value;
            _artistCollection = artistCollection;
            _galleryCollection = pictureCollection;
        }

        public async Task<(double totalPages,int totalRecords, IReadOnlyList<Picture> data)> GetPagedGallery(int page, int pageSize)
        {
            return await _galleryCollection.GetPagedResults(page, pageSize);
        }
    }
}
