using CinemaPro.Enums;
using CinemaPro.Models;
using CinemaPro.Models.CinemaPro;
using CinemaPro.Models.TMDB;
using CinemaPro.Models.ViewModels;
using CinemaPro.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CinemaPro.Controllers;
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IRemoteMovieService _tmdbMovieService;

    public HomeController(ILogger<HomeController> logger, IRemoteMovieService tmdbMovieService)
    {
        _logger = logger;
        _tmdbMovieService = tmdbMovieService;
    }

    public async Task<IActionResult> Index()
    {
        const int count = 16;
        var data = new LandingPageVM()
        {
            NowPlaying = await _tmdbMovieService.SearchMoviesAsync(MovieCategory.now_playing, count),
            Popular = await _tmdbMovieService.SearchMoviesAsync(MovieCategory.popular, count),
            TopRated = await _tmdbMovieService.SearchMoviesAsync(MovieCategory.top_rated, count),
            Upcoming = await _tmdbMovieService.SearchMoviesAsync(MovieCategory.upcoming, count)
        };
        return View(data);
    }

    public IActionResult Attribution()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
