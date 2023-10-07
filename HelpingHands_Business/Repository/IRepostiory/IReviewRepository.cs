using HelpingHands_DataAccess;

namespace HelpingHands_Business.Repository.IRepostiory
{
    public interface IReviewRepository : IRepository<Review>
    {
      
        Task<Review> UpdateAsync(Review entity);
  
    }
}
