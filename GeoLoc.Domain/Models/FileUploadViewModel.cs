using Microsoft.AspNetCore.Http;

namespace GeoLoc.Domain.Models;

public class FileUploadViewModel
{
    public IFormFile File { get; set; }
}