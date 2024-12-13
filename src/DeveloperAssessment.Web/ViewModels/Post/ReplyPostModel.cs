namespace DeveloperAssessment.Web.ViewModels.Post;

public class ReplyPostModel
{
    public int BlogId { get; init; }
    public Guid CommentId { get; init; }
    public string Message { get; init; }
}