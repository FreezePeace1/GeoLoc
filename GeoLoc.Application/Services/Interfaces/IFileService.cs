using GeoLoc.Domain.Dtos;
using GeoLoc.Domain.Models;

namespace GeoLoc.Application.Services.Interfaces;

public interface IFileService
{
    public Task<ResponseDto> Upload(FileUploadViewModel model);

    public ResponseDto<List<string>> ListFiles();

    public ResponseDto<byte[]> Download(string filename);

    public Task<ResponseDto<List<string[]>>> Read(string filename);

}