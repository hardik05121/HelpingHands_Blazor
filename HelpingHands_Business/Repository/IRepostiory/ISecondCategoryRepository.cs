using HelpingHands_DataAccess;

namespace HelpingHands_Business.Repository.IRepostiory
{
    public interface ISecondCategoryRepository : IRepository<SecondCategory>
    {
      
        Task<SecondCategory> UpdateAsync(SecondCategory entity);
  
    }
}
