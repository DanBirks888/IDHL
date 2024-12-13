using DeveloperAssessment.Web.DomainModels;
using DeveloperAssessment.Web.Extensions;
using DeveloperAssessment.Web.Interfaces;
using DeveloperAssessment.Web.ViewModels.Post;
using Microsoft.Extensions.Caching.Memory;

namespace DeveloperAssessment.Web.Services;

public class BlogService : IBlogService
{
    private readonly IFileService _fileService;
    private readonly IMemoryCache _memoryCache;
    private readonly ILogger<BlogService> _logger;
    private readonly IBlogRepository _blogRepository;

    public BlogService(ILogger<BlogService> logger,
        IFileService fileService,
        IMemoryCache memoryCache,
        IBlogRepository blogRepository)
    {
        _logger = logger;
        _fileService = fileService;
        _memoryCache = memoryCache;
        _blogRepository = blogRepository;
    }

    public BlogPost Get(int id = 0)
    {
        var blogsViewModel = _blogRepository.GetBlogs();

        return id == 0
            ? blogsViewModel.BlogPosts.FirstOrDefault() ?? new BlogPost()
            : blogsViewModel.BlogPosts.FirstOrDefault(blog => blog.Id == id) ?? new BlogPost();
    }

    public BlogList GetAll()
    {
        return _blogRepository.GetBlogs();
    }

    public BlogPost AddCommentToBlogPost(CommentPostModel commentPostModel)
    {
        var blogList = _blogRepository.GetBlogs();
        blogList
            .BlogPosts
            .FirstOrDefault(blog => blog.Id == commentPostModel.BlogId)
            ?.Comments
            .Add(commentPostModel.ToComment());

        _blogRepository.SaveBlogs(blogList);
        return _blogRepository.GetById(commentPostModel.BlogId);
    }

    public BlogPost ReplyToBlogPost(ReplyPostModel replyPostModel)
    {
        var blogList = _blogRepository.GetBlogs();
        blogList
            .BlogPosts
            .FirstOrDefault(blog => blog.Id == replyPostModel.BlogId)
            ?.Comments
            .FirstOrDefault(comment => comment.Id == replyPostModel.CommentId)
            ?.Replies.Add(replyPostModel.Message);

        _blogRepository.SaveBlogs(blogList);
        return _blogRepository.GetById(replyPostModel.BlogId);
    }
}