using DeveloperAssessment.Web.DomainModels;
using DeveloperAssessment.Web.ViewModels.Post;

namespace DeveloperAssessment.Web.Interfaces;

// i.e. DeveloperAssessment.Core for all contracts to be contained in Domain
public interface IBlogService
{
    BlogPost Get(int id);
    BlogList GetAll();
    BlogPost AddCommentToBlogPost(CommentPostModel commentPostModel);
    BlogPost ReplyToBlogPost(ReplyPostModel replyPostModel);
}
