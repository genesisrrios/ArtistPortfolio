using ArtistPortfolio.ViewModel;
using DAL.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ArtistPortfolio.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly ArtistService service;


        public IndexViewModel ViewModel { get; set; } = new();

        public IndexModel(ILogger<IndexModel> logger,ArtistService artistService)
        {
            _logger = logger;
            service = artistService;
        }

        public async Task OnGet()
        {
            ViewModel.Pictures = await service.GetGallery(1,15);
            Console.WriteLine("Programming is hell");
        }
    }
}