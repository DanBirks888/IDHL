using DeveloperAssessment.Web.Extensions;
using DeveloperAssessment.Web.Interfaces;
using DeveloperAssessment.Web.ViewModels.Post;
using Microsoft.AspNetCore.Mvc;

namespace DeveloperAssessment.Web.Controllers;

public class BlogApiController : Controller
{
    private readonly IBlogService _blogService;
    private readonly IFileUploadService _fileUploadService;

    public BlogApiController(IBlogService blogService,
        IFileUploadService fileUploadService)
    {
        _blogService = blogService;
        _fileUploadService = fileUploadService;
    }

    public async Task<IActionResult> SubmitComment([FromForm] CommentPostModel commentPostModel)
    {
        commentPostModel.FormUploadId = _fileUploadService.Save(commentPostModel.FileUpload);

        var blogPost = _blogService.AddCommentToBlogPost(commentPostModel);
        return Ok(blogPost.ToViewModel());
    }

    public async Task<IActionResult> ReplyToComment([FromBody] ReplyPostModel replyPostModel)
    {
        var blogPost = _blogService.ReplyToComment(replyPostModel);
        return Ok(blogPost.ToViewModel());
    }
}