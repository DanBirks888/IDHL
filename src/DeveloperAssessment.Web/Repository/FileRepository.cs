using System.Text.Json;
using DeveloperAssessment.Web.DomainModels;
using DeveloperAssessment.Web.Interfaces;
using DeveloperAssessment.Web.ViewModels.FileUploads;
using Microsoft.Extensions.Caching.Memory;

namespace DeveloperAssessment.Web.Repository;

public class FileRepository
{
    private readonly IFileService _fileService;
    private readonly IMemoryCache _memoryCache;
    private readonly ILogger<BlogRepository> _logger;

    private readonly string _cacheKey = "blog";

    public FileUploads GetUploadedFiles()
    {
        // if (_memoryCache.TryGetValue(_cacheKey, out FileUploads? cachedBlogList))
        // {
        //     return cachedBlogList ?? new FileUploads();
        // } 

        try
        {
            var stream = _fileService.GetFileStream(Constants.FileNames.FileUploads);
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var fileUploads = JsonSerializer.Deserialize<FileUploads>(stream, options) ?? new FileUploads();

            // _memoryCache.Set(_cacheKey, fileUploads, DateTimeOffset.Now.AddHours(1));
            
            return fileUploads;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching File Uploads from file");
            return new FileUploads();
        }
    }

    public void SaveFileUploads(FileUploads fileUploads)
    {
        try
        {
            var jsonFileUploads = JsonSerializer.Serialize(fileUploads);
            _fileService.WriteToFile(Constants.FileNames.FileUploads, jsonFileUploads);
            // _memoryCache.Set(_cacheKey, fileUploads, DateTimeOffset.Now.AddHours(1));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error saving blogs to file");
        }
    }
}