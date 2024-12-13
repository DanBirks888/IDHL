using DeveloperAssessment.Web.DomainModels;
using DeveloperAssessment.Web.Extensions;

namespace DeveloperAssessment.Web.ViewModels.Blog;

public class BlogPostViewModel
{
    public int Id { get; init; }
    public DateTime Date { get; init; }
    public string Title { get; init; }
    public string Image { get; init; }

    public string HtmlContent { get; init; }
    public List<CommentViewModel> Comments { get; init; }

}