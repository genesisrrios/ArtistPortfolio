using ArtistPortfolio.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
namespace ArtistPortfolio.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexViewModel ViewModel { get; set; } = new();

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public async Task OnGet([FromQuery]int page = 1, int pageSize = 15)
        {
            //var (totalPages, gallery) = await _service.GetPagedGallery(page, pageSize);
            //var provider = new PhysicalFileProvider(_webHostEnvironment.WebRootPath);
            //var contents = provider.GetDirectoryContents(Path.Combine("images", "gallery"));
            //ViewModel.TotalPages = totalPages;
            //ViewModel.Page = page;
            //foreach (var image in gallery)
            //{
            //    ViewModel.Pictures.Add(new PictureViewModel
            //    {
            //        Description = image.Description,
            //        Name = contents.Where(c => c.Name.Contains(image.Name)).First().Name
            //    });
            //}
        }
    }
}

