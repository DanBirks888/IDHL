using System.Text.Json;
using DeveloperAssessment.Web.DomainModels;
using DeveloperAssessment.Web.Extensions;
using DeveloperAssessment.Web.Interfaces;
using Microsoft.Extensions.Caching.Memory;

namespace DeveloperAssessment.Web.Repository;

public class FileRepository : IFileRepository
{
    private readonly IFileService _fileService;
    private readonly IMemoryCache _memoryCache;
    private readonly ILogger<BlogRepository> _logger;

    private readonly string _cacheKey = "file";

    public FileRepository(IFileService fileService, IMemoryCache memoryCache, ILogger<BlogRepository> logger)
    {
        _fileService = fileService;
        _memoryCache = memoryCache;
        _logger = logger;
    }

    public FileUploads GetAll()
    {
        if (_memoryCache.TryGetValue(_cacheKey, out FileUploads? fileUploads))
        {
            return fileUploads ?? new FileUploads();
        }

        return _fileService.DeserializeFromFile<FileUploads>(Constants.FileNames.FileUploads);
    }

    public FileUpload GetById(Guid id)
    {
        return GetAll().Files.FirstOrDefault() ?? new FileUpload();
    }

    public void Save(FileUpload fileUpload)
    {
        var newFileUploads = GetAll();
        newFileUploads.Files.Add(fileUpload);
        var jsonFileUpload = JsonSerializer.Serialize(newFileUploads);
        _fileService.WriteToFile(Constants.FileNames.FileUploads, jsonFileUpload);
    }
}