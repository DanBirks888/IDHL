using System.Collections;

namespace DeveloperAssessment.Web.DomainModels;

public class BlogList
{
    public List<BlogPost> BlogPosts { get; init; }

    public BlogList()
    {
        BlogPosts = new();
    }

}