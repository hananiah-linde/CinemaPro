using CinemaPro.Models.CinemaPro;
using CinemaPro.Models.TMDB;

namespace CinemaPro.Services.Interfaces;

public interface IDataMappingService
{
    Task<Movie> MapMovieDetailAsync(MovieDetail movie);
    ActorDetail MapActorDetail(ActorDetail actor);
}
