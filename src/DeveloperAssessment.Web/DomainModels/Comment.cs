namespace DeveloperAssessment.Web.DomainModels;

public class Comment
{
    public Guid Id { get; init; }
    public string DownloadName { get; init; } = string.Empty;
    public string? DownloadUrl { get; init; }
    public string Name { get; init; } = string.Empty;
    public DateTime Date { get; init; }
    public string EmailAddress { get; init; } = string.Empty;
    public string Message { get; init; } = string.Empty;
    public List<string> Replies { get; init; } = new();
}