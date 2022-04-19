using CinemaPro.Models.TMDB;

namespace CinemaPro.Services.Interfaces;

public interface IRemoteMovieService
{
    Task<MovieDetail> MovieDetailAsync(int id);
    Task<SearchMovies> SearchMoviesAsync(string searchTerm, int count);
}
