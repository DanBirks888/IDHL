using DeveloperAssessment.Web.DomainModels;

namespace DeveloperAssessment.Web.ViewModels.Post;

public class CommentPostModel
{
    public int BlogId { get; init; }
    public string Name { get; init; }
    public string Email { get; init; }
    public string Message { get; init; }
    
}