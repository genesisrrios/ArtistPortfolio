using DAL.DTO;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface ITokenService
    {
        string BuildToken(string key, string issuer, ArtistDTO user);
        bool ValidateToken(string key, string issuer, string audience, string token);
    }
}
