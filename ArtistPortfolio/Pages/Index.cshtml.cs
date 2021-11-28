using DAL.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ArtistPortfolio.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly ArtistService service;

        public IndexModel(ILogger<IndexModel> logger,ArtistService artistService)
        {
            _logger = logger;
            service = artistService;
        }

        public async Task OnGet()
        {
            var test = await service.GetAll();
            Console.WriteLine(test.FirstOrDefault().Name);
        }
    }
}