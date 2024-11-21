using GeoLoc.Application.Services.Interfaces;
using GeoLoc.Domain.Dtos;
using GeoLoc.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace GeoLoc.Controllers;

public class MapController : Controller
{
    private readonly IMapService _mapService;

    public MapController(IMapService mapService)
    {
        _mapService = mapService;
    }

    [HttpGet]
    public async Task<ActionResult<ResponseDto<List<LatAndLongDataModel>>>> GetMap()
    {
        var response = await _mapService.GetLatAndLongData(
            @"/home/alexander/RiderProjects/GeoLoc/GeoLoc.Api/wwwroot/uploads/α864¬¬_131017_0000 - 131017_2359.txt");

        if (response.IsSucceed)
        {
            return View(response);
        }

        if (!response.IsSucceed)
        {
            var error = response.ErrorMessage;
            ModelState.AddModelError(string.Empty, error.ToString());
        }

        return View(response);
    }
}