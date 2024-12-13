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

    private readonly string _cacheKey = "blog";

    public BlogRepository(IFileService fileService, IMemoryCache memoryCache, ILogger<BlogRepository> logger)
    {
        _fileService = fileService;
        _memoryCache = memoryCache;
        _logger = logger;
    }

    public BlogList GetBlogs()
    {
        if (_memoryCache.TryGetValue(_cacheKey, out BlogList? cachedBlogList))
        {
            return cachedBlogList ?? new BlogList();
        }

        try
        {
            var blogList = _fileService.DeserializeFromFile<BlogList>(Constants.FileNames.BlogData);
            _memoryCache.Set(_cacheKey, blogList, DateTimeOffset.Now.AddHours(1));
            return blogList;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching blogs from file");
            return new BlogList();
        }
    }

    public BlogPost GetById(int id)
    {
        if (_memoryCache.TryGetValue(_cacheKey, out BlogPost? cachedBlogList))
        {
            return cachedBlogList ?? new BlogPost();
        }

        try
        {
            var blogList = _fileService.DeserializeFromFile<BlogList>(Constants.FileNames.BlogData);
            _memoryCache.Set(_cacheKey, blogList, DateTimeOffset.Now.AddHours(1));
            return blogList.BlogPosts.FirstOrDefault(blog => blog.Id == id) ?? new();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching blogs from file");
            return new BlogPost();
        }
    }

    public void SaveBlogs(BlogList blogList)
    {
        try
        {
            var jsonBlogs = JsonSerializer.Serialize(blogList);
            _fileService.WriteToFile(Constants.FileNames.BlogData, jsonBlogs);
            _memoryCache.Set(_cacheKey, blogList, DateTimeOffset.Now.AddHours(1));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error saving blogs to file");
        }
    }
}