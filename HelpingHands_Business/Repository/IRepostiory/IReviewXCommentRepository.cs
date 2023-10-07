using HelpingHands_DataAccess;

namespace HelpingHands_Business.Repository.IRepostiory
{
    public interface IReviewXCommentRepository : IRepository<ReviewXComment>
    {
      
        Task<ReviewXComment> UpdateAsync(ReviewXComment entity);
  
    }
}
