using DeveloperAssessment.Web.DomainModels;
using DeveloperAssessment.Web.ViewModels.Blog;
using DeveloperAssessment.Web.ViewModels.FileUploads;
using DeveloperAssessment.Web.ViewModels.Post;

namespace DeveloperAssessment.Web.Extensions;

public static class BlogExtensions
{
    public static BlogListViewModel ToViewModel(this BlogList? blogList)
    {
        if (blogList == null)
        {
            return new BlogListViewModel();
        }

        return new BlogListViewModel
        {
            BlogPosts = blogList.BlogPosts
                .Select(ToViewModel)
                .ToList()
        };
    }

    public static BlogPostViewModel ToViewModel(this BlogPost? blogPost)
    {
        if (blogPost == null)
        {
            return new BlogPostViewModel();
        }

        return new BlogPostViewModel
        {
            Id = blogPost.Id,
            Date = blogPost.Date,
            Title = blogPost.Title,
            HtmlContent = blogPost.HtmlContent,
            Image = string.IsNullOrEmpty(blogPost.Image) ? "https://placehold.co/600x400" : blogPost.Image,
            Comments = blogPost.Comments
                .Select(ToViewModel)
                .ToList()
        };
    }


    public static CommentViewModel ToViewModel(this Comment? comment)
    {
        if (comment == null)
        {
            return new CommentViewModel();
        }

        return new CommentViewModel
        {
            Id = comment.Id,
            Name = comment.Name,
            Date = comment.Date,
            EmailAddress = comment.EmailAddress,
            Message = comment.Message,
            Replies = comment.Replies,
            FormattedDate = $"({comment.Date.ToLongDateString()} - {comment.Date.ToLongTimeString()})",
        };
    }

    public static Comment ToComment(this CommentPostModel? comment)
    {
        if (comment == null)
        {
            return new Comment();
        }

        return new Comment
        {
            Id = Guid.NewGuid(),
            FileUploadId = comment.FormUploadId,
            Name = comment.Name,
            Date = DateTime.Now,
            EmailAddress = comment.Email,
            Message = comment.Message,
            Replies = []
        };
    }

}