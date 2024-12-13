namespace DeveloperAssessment.Web.DomainModels;

public class FileUpload
{
    public Guid Id { get; init; }

    public string Name { get; init; }
    public byte[] Bytes{ get; init; }
}