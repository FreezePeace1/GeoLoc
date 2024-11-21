using GeoLoc.Application.Resources;
using GeoLoc.Application.Services.Interfaces;
using GeoLoc.Domain.Dtos;
using GeoLoc.Domain.Enums;
using GeoLoc.Domain.Models;
using Microsoft.AspNetCore.Hosting;
using Serilog;

namespace GeoLoc.Application.Services;

public class MapService : IMapService
{
    private readonly ILogger _logger;
    private readonly IWebHostEnvironment _hostEnvironment;

    public MapService(ILogger logger,IWebHostEnvironment hostEnvironment)
    {
        _logger = logger;
        _hostEnvironment = hostEnvironment;
    }
    
    public async Task<ResponseDto<List<LatAndLongDataModel>>> GetLatAndLongData(string filename)
    {
        try
        {
            if (string.IsNullOrEmpty(filename))
            {
                return new ResponseDto<List<LatAndLongDataModel>>()
                {
                    ErrorMessage = ErrorMessage.FilenameIsNotProvided,
                    ErrorCode = (int)ErrorCode.FilenameIsNotProvided
                };
            }

            string filePath = Path.Combine(_hostEnvironment.WebRootPath, "uploads", filename);

            if (!System.IO.File.Exists(filePath))
            {
                return new ResponseDto<List<LatAndLongDataModel>>()
                {
                    ErrorMessage = ErrorMessage.FileIsNotFound,
                    ErrorCode = (int)ErrorCode.FileIsNotFound
                };
            }

            var fileContent = new List<LatAndLongDataModel>();

            using (var reader = new StreamReader(filePath))
            {
                string line = null;
                while (null != (line = await reader.ReadLineAsync()))
                {
                    string[] values = line.Split(',').Where(x => x != ";" && x != "&REPORT").ToArray();

                    var model = new LatAndLongDataModel()
                    {
                        Longitude = $"{values[5].Substring(1,2)}.{values[5].Substring(3,2)}{values[5].Substring(6,2)}",
                        Latitude = $"{values[3].Substring(1,2)}.{values[3].Substring(3,2)}{values[3].Substring(6,2)}"
                    };

                    fileContent.Add(model);
                }
            }

            return new ResponseDto<List<LatAndLongDataModel>>()
            {
                Data = fileContent
            };
        }
        catch (Exception e)
        {
            _logger.Error(e.Message);

            return new ResponseDto<List<LatAndLongDataModel>>()
            {
                ErrorMessage = ErrorMessage.FailedToGetDataForMap,
                ErrorCode = (int)ErrorCode.FailedToGetDataForMap
            };
        }
    }
}