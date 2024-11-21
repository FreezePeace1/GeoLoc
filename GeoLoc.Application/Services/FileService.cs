using System.Globalization;
using GeoLoc.Application.Resources;
using GeoLoc.Application.Services.Interfaces;
using GeoLoc.Domain.Dtos;
using GeoLoc.Domain.Enums;
using GeoLoc.Domain.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.VisualBasic.FileIO;
using Serilog;

namespace GeoLoc.Application.Services;

public class FileService : IFileService
{
    private readonly ILogger _logger;
    private readonly IWebHostEnvironment _environment;

    public FileService(ILogger logger, IWebHostEnvironment environment)
    {
        _logger = logger;
        _environment = environment;
    }

    public async Task<ResponseDto> Upload(FileUploadViewModel model)
    {
        try
        {
            if (model.File != null && model.File.Length > 0)
            {
                // Define the upload folder
                string uploadPath = Path.Combine(_environment.WebRootPath, "uploads");
                // Create the folder if it doesn't exist
                if (!Directory.Exists(uploadPath))
                {
                    Directory.CreateDirectory(uploadPath);
                }

                // Generate the file path
                string filePath = Path.Combine(uploadPath, model.File.FileName);
                // Save the file to the specified location
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await model.File.CopyToAsync(stream);
                }

                return new ResponseDto()
                {
                    SuccessMessage = SuccessMessage.UploadedSuccessfully
                };
            }

            return new ResponseDto()
            {
                ErrorMessage = ErrorMessage.FilePickedWrongly,
                ErrorCode = (int)ErrorCode.FilePickedWrongly
            };
        }
        catch (Exception e)
        {
            _logger.Error(e.Message);

            return new ResponseDto()
            {
                ErrorMessage = ErrorMessage.UploadedFailed,
                ErrorCode = (int)ErrorCode.UploadingFileIsFailed
            };
        }
    }

    public ResponseDto<List<string>> ListFiles()
    {
        try
        {
            var files = new List<string?>();

            string uploadPath = Path.Combine(_environment.WebRootPath, "uploads");
            if (!Directory.Exists(uploadPath))
            {
                return new ResponseDto<List<string>>()
                {
                    Data = files,
                    ErrorMessage = ErrorMessage.DirectoryDoesNotExist,
                    ErrorCode = (int)ErrorCode.DirectoryDoesNotExist
                };
            }

            files = Directory.GetFiles(uploadPath).Select(Path.GetFileName).ToList();

            return new ResponseDto<List<string>>()
            {
                Data = files,
                SuccessMessage = SuccessMessage.ReceivingFilesIsSuccessful
            };
        }
        catch (Exception e)
        {
            _logger.Error(e.Message);

            return new ResponseDto<List<string>>()
            {
                ErrorMessage = ErrorMessage.RecevingIsFailed,
                ErrorCode = (int)ErrorCode.ReceivingIsFailed
            };
        }
    }

    public ResponseDto<byte[]> Download(string filename)
    {
        try
        {
            if (string.IsNullOrEmpty(filename))
            {
                return new ResponseDto<byte[]>()
                {
                    ErrorMessage = ErrorMessage.FilenameIsNotProvided,
                    ErrorCode = (int)ErrorCode.FilenameIsNotProvided
                };
            }
            
            string filePath = Path.Combine(_environment.WebRootPath, "uploads", filename);
            
            if (!System.IO.File.Exists(filePath))
            {
                return new ResponseDto<byte[]>()
                {
                    ErrorMessage = ErrorMessage.FileIsNotFound,
                    ErrorCode = (int)ErrorCode.FileIsNotFound
                };
            }
            
            byte[] fileBytes = System.IO.File.ReadAllBytes(filePath);

            return new ResponseDto<byte[]>()
            {
                Data = fileBytes,
                SuccessMessage = "Скачивание файла успешно"
            };
        }
        catch (Exception e)
        {
            _logger.Error(e.Message);

            return new ResponseDto<byte[]>()
            {
                ErrorMessage = ErrorMessage.DownloadingFileIsFailed,
                ErrorCode = (int)ErrorCode.DownloadingFileIsFailed
            };
        }
    }

    public async Task<ResponseDto<List<string[]>>> Read(string filename)
    {
        try
        {
            if (string.IsNullOrEmpty(filename))
            {
                return new ResponseDto<List<string[]>>()
                {
                    ErrorMessage = ErrorMessage.FilenameIsNotProvided,
                    ErrorCode = (int)ErrorCode.FilenameIsNotProvided
                };
            }
            
            string filePath = Path.Combine(_environment.WebRootPath, "uploads", filename);
            
            if (!System.IO.File.Exists(filePath))
            {
                return new ResponseDto<List<string[]>>()
                {
                    ErrorMessage = ErrorMessage.FileIsNotFound,
                    ErrorCode = (int)ErrorCode.FileIsNotFound
                };
            }
            
            var fileContent = new List<string[]>();

            using (var reader = new StreamReader(filePath))
            {
                string line = null;
                while (null != (line = await reader.ReadLineAsync()))
                {
                    string[] values = line.Split(',').Where(x => x != ";" && x != "&REPORT").ToArray();
                    
                    values[1] = $"{values[1].Substring(0,2)}.{values[1].Substring(2,2)}.20{values[1].Substring(4,2)}";
                    values[2] = $"{values[2].Substring(0, 2)}:{values[2].Substring(2, 2)}:{values[2].Substring(4, 2)}";
                    
                    fileContent.Add(values);
                }
            }

            return new ResponseDto<List<string[]>>()
            {
                Data = fileContent
            };

        }
        catch (Exception e)
        {
            _logger.Error(e.Message);

            return new ResponseDto<List<string[]>>()
            {
                ErrorMessage = ErrorMessage.ReadingFileIsFailed,
                ErrorCode = (int)ErrorCode.ReadingFileIsFailed
            };
        }
    }
}