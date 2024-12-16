using System.ComponentModel.DataAnnotations;

namespace DeveloperAssessment.Web.ViewModels.Post;

public class ReplyPostModel
{
    [Required(ErrorMessage = "Blog ID is required.")]
    public int BlogId { get; init; }

    [Required(ErrorMessage = "Comment ID is required.")]
    public Guid CommentId { get; init; }

    [Required(ErrorMessage = "Message cannot be empty.")]
    [StringLength(500, ErrorMessage = "Message cannot exceed 500 characters.")]
    public string Message { get; init; } = string.Empty;
}