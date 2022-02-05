using ArtistPortfolio.ViewModel;
using AutoMapper;
using DAL.DTO;
using DAL.Interfaces;
using DAL.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace ArtistPortfolio.Pages
{
    public class AdminLoginModel : PageModel
    {
        private readonly AdminService _adminService;
        private readonly ArtistService _artistService;
        private readonly ITokenService _tokenService;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        public AdminLoginModel(AdminService adminService, ArtistService artistService, ITokenService tokenService, IConfiguration config, IMapper mapper)
        {
            _adminService = adminService;
            _artistService = artistService;
            _tokenService = tokenService;
            _configuration = config;
            _mapper = mapper;
        }
        public void OnGet()
        {
        }
        [BindProperty]
        public LoginViewModel LoginInfo { get; set; }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            bool passwordValidated = await _adminService
                .ValidatePassword(LoginInfo.Password,LoginInfo.UserId);

            if (!passwordValidated)
                return Page();

            var artist = await _artistService.GetArtist(LoginInfo.UserId);
            var mappedArtist = _mapper.Map<ArtistDTO>(artist);
            var generatedToken = _tokenService.BuildToken(_configuration["Jwt:Key"].ToString(), _configuration["Jwt:Issuer"].ToString(), mappedArtist);
            if(generatedToken == null) return (RedirectToAction("Error"));

            HttpContext.Session.SetString("Token", generatedToken);
            TempData["Token"] = generatedToken;
            return RedirectToPage("./Admin");
        }
    }
}
