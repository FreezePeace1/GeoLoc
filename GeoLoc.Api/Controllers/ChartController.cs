using GeoLoc.Application.Services.Interfaces;
using GeoLoc.Domain.Dtos;
using GeoLoc.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace GeoLoc.Controllers;

public class ChartController : Controller
{
    private readonly IChartService _chartService;

    public ChartController(IChartService chartService)
    {
        _chartService = chartService;
    }

    [HttpGet]
    public async Task<ActionResult<ResponseDto<List<SpeedAndTimeDataModel>>>> GetSpeedAndTimeChart()
    {
        var response = await _chartService.GetSpeedAndTimeData(
            @"/home/alexander/RiderProjects/GeoLoc/GeoLoc.Api/wwwroot/uploads/α864¬¬_131017_0000 - 131017_2359.txt");

        ViewBag.Speed = response.Data.SelectMany(x => x.Speed).ToArray();
        ViewBag.Time = response.Data.SelectMany(x => x.Time).ToList();
        
        
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


    [HttpGet]
    public async Task<ActionResult<ResponseDto<List<LatAndLongDataModel>>>> GetLatitudeAndLongitudeChart()
    {
        var response = await _chartService.GetLatitudeAndLongitudeData(
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