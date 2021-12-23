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
using DAL;
using MongoDB.Bson;

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

        public async Task<(double totalRecords, int totalPages, IReadOnlyList<Picture> data)> GetPagedGallery(int page, int pageSize, FiltersTypes.ArtistFilter filterParameters = default)
        {
            var builder = Builders<Picture>.Filter;
            var filter = builder.Empty;

            if(filterParameters is not null)
            {
                if (!String.IsNullOrEmpty(filterParameters.Description))
                {
                    var descriptionFilter = builder.AnyIn("Description", new string[] { filterParameters.Description });
                    filter &= descriptionFilter;
                }

                if (filterParameters.Properties is not null && filterParameters.Properties.Any())
                {
                    var propertyFilter = builder.AnyIn("Properties", filterParameters.Properties);
                    filter &= propertyFilter;
                }
            }

            return await _galleryCollection.GetPagedResults(page, pageSize,filter);
        }
    }
}
