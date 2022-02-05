using System.ComponentModel.DataAnnotations;

namespace ArtistPortfolio.ViewModel
{
    public class LoginViewModel
    {
        [Required, MinLength(3)]
        public string UserId { get; set; }
        [Required, MinLength(8)]
        public string Password { get; set; }
    }
}
