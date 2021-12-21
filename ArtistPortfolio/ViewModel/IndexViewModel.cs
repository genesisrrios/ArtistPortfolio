using System.Collections.Generic;

namespace ArtistPortfolio.ViewModel;
public class IndexViewModel
{
    public List<PictureViewModel> Pictures { get; set; } = new List<PictureViewModel>();
    public int Page { get; set; } = 1;
    public int TotalPages { get; set; }
}
