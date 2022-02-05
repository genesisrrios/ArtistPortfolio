using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Services
{
    public class AdminService
    {
        private readonly Security _securityService;
        private readonly ArtistService _artistService;
        public AdminService(Security security, ArtistService artistService)
        {
            _securityService = security;
            _artistService = artistService;
        }
        public async Task<bool> ValidatePassword(string password, string userId)
        {
            var artist = await _artistService.GetArtist(userId);
            if (artist == null) return false;
            return _securityService.ValidatePassword(artist.Password, password);
        }
    }
}
