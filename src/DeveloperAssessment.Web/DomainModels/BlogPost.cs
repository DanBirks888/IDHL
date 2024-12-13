using DeveloperAssessment.Web.ViewModels.Blog;

namespace DeveloperAssessment.Web.DomainModels;

public class BlogPost
{
    public int Id { get; init; }
    public DateTime Date { get; init; }
    public string Title { get; init; }
    public string Image { get; init; }

    public string HtmlContent { get; init; }
    public List<Comment> Comments { get; init; }

    public BlogPost()
    {
        Comments = new();
    }
}