using ArtistPortfolio.DAL.Models;

namespace ArtistPortfolio.ViewModel;
public class IndexViewModel
{
    public List<PictureViewModel> Pictures { get; set; } = new List<PictureViewModel>();
}
