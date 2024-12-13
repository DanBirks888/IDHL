using DeveloperAssessment.Web.DomainModels;
using DeveloperAssessment.Web.Extensions;

namespace DeveloperAssessment.Web.ViewModels.Blog;

public class BlogListViewModel
{
    public List<BlogPostViewModel> BlogPosts { get; init; }

    public BlogListViewModel()
    {
        BlogPosts = new();
    }

    public BlogListViewModel(BlogList blogList)
    {
        foreach (var blog in blogList.BlogPosts)
        {
            BlogPosts?.Add(blog.ToViewModel());
        }
    }
}