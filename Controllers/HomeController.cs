using CinemaPro.Models;
using CinemaPro.Models.CinemaPro;
using CinemaPro.Models.TMDB;
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

    public async Task<IActionResult> Index(int id)
    {
        return View();
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
