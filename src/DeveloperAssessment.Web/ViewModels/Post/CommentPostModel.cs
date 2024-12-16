using System.ComponentModel.DataAnnotations;

namespace DeveloperAssessment.Web.ViewModels.Post;

public class CommentPostModel
{
    [Required(ErrorMessage = "Blog ID is required.")]
    public int BlogId { get; init; }

    [Required(ErrorMessage = "Name is required.")]
    [StringLength(50, ErrorMessage = "Name cannot exceed 50 characters.")]
    public string Name { get; init; }

    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "Invalid email address format.")]
    public string Email { get; init; }

    [Required(ErrorMessage = "Message cannot be empty.")]
    [StringLength(500, ErrorMessage = "Message cannot exceed 500 characters.")]
    public string Message { get; init; }

    public IFormFile? FileUpload { get; init; }

    public string DownloadName { get; set; } = string.Empty;

    public string DownloadUrl { get; private set; } = string.Empty;

    public void AppendDownloadUrl(string domain, string downloadUrl)
    {
        DownloadUrl = $"https://{domain}/{downloadUrl}";
    }
}