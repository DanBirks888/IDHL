namespace DeveloperAssessment.Web.DomainModels;

public class Comment
{
    public Guid Id { get; init; }
    public Guid? FileUploadId { get; init; }
    public string Name { get; init; }
    public DateTime Date { get; init; }
    public string EmailAddress { get; init; }
    public string Message { get; init; }
    public List<string> Replies { get; init; } = new();
}