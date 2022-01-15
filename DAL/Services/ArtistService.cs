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
using System.Text.RegularExpressions;

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
                    var queryExpr = new BsonRegularExpression(new Regex(filterParameters.Description, RegexOptions.None));
                    var descriptionFilter = builder.Regex("Description", queryExpr);
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
        public IEnumerable<string> GetCategories()
        {
            var result = new List<string>();

            var categories = _galleryCollection.FilterBy(_ => true).Select(x => x.Properties);
            
            foreach(var category in categories)
                if(category is not null)
                    result.AddRange(category.Select(x => x));

            return result.Distinct();
        }
    }
}
