using CinemaPro.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CinemaPro.Controllers;
public class ActorsController : Controller
{

    private readonly IRemoteMovieService _tmdbMovieService;
    private readonly IDataMappingService _dataMappingService;

    public ActorsController(IRemoteMovieService tmdbMovieService, IDataMappingService mappingService)
    {
        _tmdbMovieService = tmdbMovieService;
        _dataMappingService = mappingService;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        var actor = await _tmdbMovieService.ActorDetailAsync(id);
        actor = _dataMappingService.MapActorDetail(actor);
        return View(actor);
    }
}

