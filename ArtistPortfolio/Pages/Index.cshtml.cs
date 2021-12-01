using ArtistPortfolio.ViewModel;
using DAL.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.FileProviders;

namespace ArtistPortfolio.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly ArtistService _service;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public IndexViewModel ViewModel { get; set; } = new();

        public IndexModel(ILogger<IndexModel> logger,ArtistService artistService, IWebHostEnvironment webHostEnvironment)
        {
            _logger = logger;
            _service = artistService;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task OnGet()
        {
            var images = await _service.GetGallery(1,15);
            var provider = new PhysicalFileProvider(_webHostEnvironment.WebRootPath);
            var contents = provider.GetDirectoryContents(Path.Combine("images", "gallery"));

            images.ForEach(x =>
            {
                ViewModel.Pictures.Add(new PictureViewModel
                {
                    Description = x.Description,
                    Name = contents.Where(c=>c.Name.Contains(x.Name)).First().Name
                });
            });
        }
    }
}