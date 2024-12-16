using DeveloperAssessment.Web.Extensions;
using DeveloperAssessment.Web.Interfaces;
using DeveloperAssessment.Web.ViewModels.Post;
using Microsoft.AspNetCore.Mvc;

namespace DeveloperAssessment.Web.Controllers;

public class BlogApiController : Controller
{
    private readonly IBlogService _blogService;
    private readonly IFileService _fileService;
    private readonly IPaginationService _paginationService;

    public BlogApiController(IBlogService blogService,
        IFileService fileService,
        IPaginationService paginationService)
    {
        _blogService = blogService;
        _fileService = fileService;
        _paginationService = paginationService;
    }

    public async Task<IActionResult> SubmitComment([FromForm] CommentPostModel commentPostModel)
    {
        if (!ModelState.IsValid)
        {
            var errors = GetErrors();
            return BadRequest(new { success = false, errors });
        }

        if (commentPostModel.FileUpload != null)
        {
            var localDownloadUrl = _fileService.SaveFileUploadToDirectory(commentPostModel.FileUpload);
            commentPostModel.AppendDownloadUrl(Request.Host.Value, localDownloadUrl);
        }

        var blogPost = _blogService.AddCommentToBlogPost(commentPostModel);
        return Ok(new { success = true, data = blogPost.ToViewModel() });
    }


    public async Task<IActionResult> ReplyToComment([FromBody] ReplyPostModel replyPostModel)
    {
        if (!ModelState.IsValid)
        {
            var errors = GetErrors();
            return BadRequest(new { success = false, errors });
        }

        var blogPost = _blogService.ReplyToComment(replyPostModel);
        return Ok(new { success = true, data = blogPost.ToViewModel() });
    }

    [HttpGet]
    public async Task<IActionResult> GetPaginatedBlogs(int pageIndex, int pageSize)
    {
        var blogs = _blogService.GetAll();
        var pagedBlogs = _paginationService.Paginate(blogs.BlogPosts, pageIndex, pageSize);
        return Ok(pagedBlogs);
    }

    private List<string> GetErrors()
    {
        return ModelState.Values
            .SelectMany(v => v.Errors)
            .Select(e => e.ErrorMessage)
            .ToList();
    }
}