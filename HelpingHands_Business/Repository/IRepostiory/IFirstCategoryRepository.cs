using HelpingHands_DataAccess;

namespace HelpingHands_Business.Repository.IRepostiory
{
    public interface IFirstCategoryRepository : IRepository<FirstCategory>
    {
      
        Task<FirstCategory> UpdateAsync(FirstCategory entity);
  
    }
}
