using System.Text.Json;
using DeveloperAssessment.Web.Interfaces;
using DeveloperAssessment.Web.ViewModels.FileUploads;

namespace DeveloperAssessment.Web.Services;

public class FileService : IFileService
{
    private readonly ILogger<FileService> _logger;
    private readonly IWebHostEnvironment _webHostEnvironment;
    private readonly JsonSerializerOptions _options = new() { PropertyNameCaseInsensitive = true };


    public FileService(ILogger<FileService> logger,
        IWebHostEnvironment webHostEnvironment)
    {
        _logger = logger;
        _webHostEnvironment = webHostEnvironment;
    }

    public Stream GetFileStream(string fileName)
    {
        var path = GetPath(fileName);
        if (!File.Exists(path))
        {
            _logger.LogError("Blog file doesn't exist");
            return Stream.Null;
        }

        return File.OpenRead(path);
    }

    public void SaveFileUpload(IFormFile? formFile)
    {
        if (formFile == null)
        {
            _logger.LogError("Form file is null");
            return;
        }

        using var memoryStream = new MemoryStream();
        formFile.CopyTo(memoryStream);

        var fileUpload = new FileUploadViewModel
        {
            Id = new Guid(),
            Name = formFile.FileName,
            ContentType = formFile.ContentType,
            Bytes = memoryStream.ToArray(),
        };
        var jsonFile = JsonSerializer.Serialize(fileUpload);
        WriteToFile(Constants.FileNames.FileUploads, jsonFile);
    }

    public T DeserializeFromFile<T>(string fileName) where T : new()
    {
        var stream = GetFileStream(fileName);
        return JsonSerializer.Deserialize<T>(stream, _options) ?? new T();
    }

    public void SerializeToFile<T>(string fileName, T data) where T : new()
    {
        var jsonData = JsonSerializer.Serialize(data);
        WriteToFile(fileName, jsonData);
    }

    public void WriteToFile(string fileName, string jsonString)
    {
        try
        {
            File.WriteAllText(GetPath(fileName), jsonString);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
        }
    }

    private string GetPath(string fileName)
    {
        return Path.Combine(_webHostEnvironment.WebRootPath, fileName);
    }
}