using CinemaPro.Models.TMDB;
using CinemaPro.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CinemaPro.Controllers;
public class MoviesController : Controller
{

    private readonly IRemoteMovieService _tmdbMovieService;

    public MoviesController(IRemoteMovieService tmdbMovieService)
    {
        _tmdbMovieService = tmdbMovieService;
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
}
