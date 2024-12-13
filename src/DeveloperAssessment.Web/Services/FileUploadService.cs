using DeveloperAssessment.Web.DomainModels;
using DeveloperAssessment.Web.Extensions;
using DeveloperAssessment.Web.Interfaces;

namespace DeveloperAssessment.Web.Services;

public class FileUploadService : IFileUploadService
{
    private readonly IFileService _fileService;
    private readonly IFileRepository _fileRepository;

    public FileUploadService(IFileService fileService,
        IFileRepository fileRepository)
    {
        _fileService = fileService;
        _fileRepository = fileRepository;
    }

    public FileUploads GetAll()
    {
        return _fileRepository.GetAll();
    }

    public Guid? Save(IFormFile? file)
    {
        if (file == null)
        {
            return null;
        }

        using var stream = new MemoryStream();
        file.CopyTo(stream);

        var fileUpload = new FileUpload
        {
            Id = Guid.NewGuid(),
            Name = file.FileName,
            Bytes = stream.ToArray()
        };

        _fileRepository.Save(fileUpload);
        return fileUpload.Id;
    }
}