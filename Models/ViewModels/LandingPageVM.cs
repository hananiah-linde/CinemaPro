using CinemaPro.Models.TMDB;

namespace CinemaPro.Models.ViewModels;

public class LandingPageVM
{
    public SearchMovies NowPlaying { get; set; }
    public SearchMovies Popular { get; set; }
    public SearchMovies TopRated { get; set; }
    public SearchMovies Upcoming { get; set; }
}
