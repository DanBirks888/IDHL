namespace DeveloperAssessment.Web.ViewModels.Blog;

public class CommentViewModel
{
    public Guid Id { get; init; }
    public string Name { get; init; }
    public DateTime Date { get; init; }
    public string FormattedDate { get; init; }
    public string EmailAddress { get; init; }
    public string Message { get; init; }
    public List<string> Replies { get; init; }
    public Guid FileUploadId { get; init; }

}