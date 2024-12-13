using DeveloperAssessment.Web.Extensions;
using DeveloperAssessment.Web.Interfaces;
using DeveloperAssessment.Web.ViewModels.Post;
using Microsoft.AspNetCore.Mvc;

namespace DeveloperAssessment.Web.Controllers;

public class BlogApiController : Controller
{
    private readonly IBlogService _blogService;

    public BlogApiController(IBlogService blogService)
    {
        _blogService = blogService;
    }

    public async Task<IActionResult> SubmitComment([FromBody] CommentPostModel commentPostModel)
    {
        var blogPost = _blogService.AddCommentToBlogPost(commentPostModel);
        return Ok(blogPost.ToViewModel());
    }
    
    public async Task<IActionResult> ReplyToComment([FromBody] ReplyPostModel replyPostModel)
    {
        var blogPost = _blogService.ReplyToBlogPost(replyPostModel);
        return Ok(blogPost.ToViewModel());
    }
   
}
