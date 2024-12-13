using DeveloperAssessment.Web.DomainModels;

namespace DeveloperAssessment.Web.Interfaces;

public interface IFileRepository
{
    FileUploads GetAll();
    FileUpload GetById(Guid id);
    void Save(FileUpload file);
}