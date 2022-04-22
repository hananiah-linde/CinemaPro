using CinemaPro.Enums;
using CinemaPro.Models.TMDB;

namespace CinemaPro.Services.Interfaces;

public interface IRemoteMovieService
{
    Task<MovieDetail> MovieDetailAsync(int id);
    Task<SearchMovies> SearchMoviesAsync(string searchTerm, int count);
    Task<SearchMovies> SearchMoviesAsync(MovieCategory category, int count);
    Task<ActorDetail> ActorDetailAsync(int id);
}
