using GeoLoc.Domain.Dtos;
using GeoLoc.Domain.Models;

namespace GeoLoc.Application.Services.Interfaces;

public interface IChartService
{
    public Task<ResponseDto<List<SpeedAndTimeDataModel>>> GetSpeedAndTimeData(string filepath);
    
    public Task<ResponseDto<List<LatAndLongDataModel>>> GetLatitudeAndLongitudeData(string filename);
}