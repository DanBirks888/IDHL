using System.Text.Json;
using DeveloperAssessment.Web.DomainModels;
using DeveloperAssessment.Web.Interfaces;
using Microsoft.Extensions.Caching.Memory;

namespace DeveloperAssessment.Web.Repository;

public class BlogRepository : IBlogRepository
{
    private readonly IFileService _fileService;
    private readonly IMemoryCache _memoryCache;
    private readonly ILogger<BlogRepository> _logger;

    private readonly string _cacheKey = "blogList";

    public BlogRepository(IFileService fileService, IMemoryCache memoryCache, ILogger<BlogRepository> logger)
    {
        _fileService = fileService;
        _memoryCache = memoryCache;
        _logger = logger;
    }

    public BlogList GetAll()
    {
        if (_memoryCache.TryGetValue(_cacheKey, out BlogList? cachedBlogList))
        {
            return cachedBlogList ?? new BlogList();
        }

        var blogList = _fileService.DeserializeFromFile<BlogList>(Constants.FileNames.BlogData);
        _memoryCache.Set(_cacheKey, blogList, DateTimeOffset.Now.AddHours(1));
        return blogList;
    }

    public BlogPost GetById(int id)
    {
        return GetAll().BlogPosts.FirstOrDefault(blog => blog.Id == id) ?? new();
    }

    public void Save(BlogList blogList)
    {
        var jsonBlogs = JsonSerializer.Serialize(blogList);
        _fileService.WriteToFile(Constants.FileNames.BlogData, jsonBlogs);
        _memoryCache.Set(_cacheKey, blogList, DateTimeOffset.Now.AddHours(1));
    }
}