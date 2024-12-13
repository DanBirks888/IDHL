using DeveloperAssessment.Web.DomainModels;
using DeveloperAssessment.Web.ViewModels.Post;

namespace DeveloperAssessment.Web.Interfaces;

public interface IFileUploadService
{
    FileUploads GetAll();
    public Guid? Save(IFormFile file);
}