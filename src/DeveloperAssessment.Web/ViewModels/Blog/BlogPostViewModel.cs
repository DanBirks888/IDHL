namespace DeveloperAssessment.Web.ViewModels.Blog;

public class BlogPostViewModel
{
    public int Id { get; init; }
    public DateTime Date { get; init; }
    public string Title { get; init; } = string.Empty;
    public string Image { get; init; } = string.Empty;

    public string HtmlContent { get; init; } = string.Empty;
    public List<CommentViewModel> Comments { get; init; } = new();
}