using System.Text.Json;
using DeveloperAssessment.Web.Interfaces;

namespace DeveloperAssessment.Web.Services;

public class FileService(
    ILogger<FileService> logger,
    IWebHostEnvironment webHostEnvironment)
    : IFileService
{
    private readonly ILogger<FileService> _logger = logger;
    private readonly IWebHostEnvironment _webHostEnvironment = webHostEnvironment;
    private readonly JsonSerializerOptions _options = new() { PropertyNameCaseInsensitive = true };


    public string SaveFileUploadToDirectory(IFormFile file)
    {
        var uniqueFolderName = Guid.NewGuid();
        var directory = GetFullPath($"uploads/{uniqueFolderName}");
        var path = $"{directory}/{file.FileName}";
        EnsureDirectoryExists(directory);
        WriteFileToPath(path, file);
        return BuildDownloadUrl(uniqueFolderName, file.FileName);
    }

    private static string BuildDownloadUrl(Guid guid, string fileName)
    {
        return $"uploads/{guid}/{fileName}";
    }

    private static void EnsureDirectoryExists(string directory)
    {
        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }
    }

    private void WriteFileToPath(string path, IFormFile file)
    {
        try
        {
            using var stream = new FileStream(path, FileMode.Create);
            file.CopyTo(stream);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Could not save file to upload directory.");
        }
    }

    public T DeserializeFromFile<T>(string fileName) where T : new()
    {
        var path = GetFullPath(fileName);
        if (!File.Exists(path))
        {
            _logger.LogError("Blog file doesn't exist");
            return new T();
        }

        using var stream = File.OpenRead(path);
        return JsonSerializer.Deserialize<T>(stream, _options) ?? new T();
    }

    public void WriteToFile(string fileName, string jsonString)
    {
        try
        {
            File.WriteAllText(GetFullPath(fileName), jsonString);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
        }
    }

    private string GetFullPath(string fileName)
    {
        return Path.Combine(_webHostEnvironment.WebRootPath, fileName);
    }
}