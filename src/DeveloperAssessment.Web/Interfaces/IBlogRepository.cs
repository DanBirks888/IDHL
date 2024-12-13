using DeveloperAssessment.Web.DomainModels;

namespace DeveloperAssessment.Web.Interfaces;

public interface IBlogRepository
{
    BlogList GetAll();
    BlogPost GetById(int id);
    void Save(BlogList blogList);
}