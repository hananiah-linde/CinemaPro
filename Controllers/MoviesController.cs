using CinemaPro.Models.TMDB;
using CinemaPro.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CinemaPro.Controllers;
public class MoviesController : Controller
{

    private readonly IRemoteMovieService _tmdbMovieService;
    private readonly IDataMappingService _tmdbMappingService;

    public MoviesController(IRemoteMovieService tmdbMovieService, IDataMappingService mappingService)
    {
        _tmdbMovieService = tmdbMovieService;
        _tmdbMappingService = mappingService;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> SearchMovies(string query)
    {
        const int count = 25;
        var data = new SearchMovies();
        data = await _tmdbMovieService.SearchMoviesAsync(query, count);


        return View(data);
    }

    [HttpGet]
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null) return NotFound();

        var movieDetail = await _tmdbMovieService.MovieDetailAsync((int)id);
        var movie = await _tmdbMappingService.MapMovieDetailAsync(movieDetail);

        if(movie == null) return NotFound();

        return View(movie);
    }
}
