using GeoLoc.Application.Services.Interfaces;
using GeoLoc.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace GeoLoc.Controllers;

public class FileController : Controller
{
    private readonly IFileService _fileService;

    public FileController(IFileService fileService)
    {
        _fileService = fileService;
    }

    // GET: File/Upload
    [HttpGet]
    public IActionResult Upload()
    {
        return View();
    }

    // POST: File/Upload
    [HttpPost]
    public async Task<IActionResult> Upload(FileUploadViewModel model)
    {
        var response = await _fileService.Upload(model);

        if (response.IsSucceed)
        {
            ViewBag.Message = "File uploaded successfully!";
            return View();
        }
        
        if (!response.IsSucceed)
        {
            ViewBag.Message = "File uploaded is failed!";
            return View();
        }
        
        return View();
    }

    // GET: File/ListFiles
    [HttpGet]
    public IActionResult ListFiles()
    {
        var response = _fileService.ListFiles();

        if (response.IsSucceed)
        {
            return View(response.Data);   
        }
        
        if (!response.IsSucceed)
        {
            var error = response.ErrorMessage;
            ModelState.AddModelError(string.Empty, error.ToString());
        }

        return View(response.Data);
    }

    // GET: File/Download?filename=example.txt
    [HttpGet]
    public IActionResult Download(string filename)
    {
        var response = _fileService.Download(filename);
        
        if (!response.IsSucceed)
        {
            var error = response.ErrorMessage;
            ModelState.AddModelError(string.Empty, error.ToString());
        }
        
        return File(response.Data, "application/octet-stream", filename);
    }

    [HttpGet]
    public async Task<IActionResult> Read(string filename)
    {
        var response = await _fileService.Read(filename);

        if (!response.IsSucceed)
        {
            var error = response.ErrorMessage;
            ModelState.AddModelError(string.Empty, error.ToString());
        }
        
        return View(response);
    }
}