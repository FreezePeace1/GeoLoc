using GeoLoc.Domain.Dtos;
using GeoLoc.Domain.Models;

namespace GeoLoc.Application.Services.Interfaces;

public interface IMapService
{
    public Task<ResponseDto<List<LatAndLongDataModel>>> GetLatAndLongData(string filename);
}