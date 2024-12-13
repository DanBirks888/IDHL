namespace DeveloperAssessment.Web.ViewModels.FileUploads;

public class FileUpload
{
    public Guid Id { get; init; }
    public byte[] Bytes { get; init; }
    public string Name { get; init; }
    public string ContentType { get; init; }
}