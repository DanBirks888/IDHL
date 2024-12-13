using DeveloperAssessment.Web.DomainModels;

namespace DeveloperAssessment.Web.Interfaces;

public interface IBlogRepository
{
    BlogList GetBlogs();
    BlogPost GetById(int id);
    void SaveBlogs(BlogList blogList);
}