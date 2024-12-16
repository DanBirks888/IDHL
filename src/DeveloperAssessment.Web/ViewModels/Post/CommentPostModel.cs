namespace DeveloperAssessment.Web.ViewModels.Post;

public class CommentPostModel
{
    public int BlogId { get; init; }
    public string Name { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
    public string Message { get; init; } = string.Empty;
    public IFormFile? FileUpload { get; init; }
    public string DownloadName { get; set; } = string.Empty;
    public string DownloadUrl { get; private set; } = string.Empty;

    public void AppendDownloadUrl(string domain, string downloadUrl)
    {
        DownloadUrl = $"https://{domain}/{downloadUrl}";
        
    }
}