using HelpingHands_DataAccess;

namespace HelpingHands_Business.Repository.IRepostiory
{
    public interface IThirdCategoryRepository : IRepository<ThirdCategory>
    {
      
        Task<ThirdCategory> UpdateAsync(ThirdCategory entity);
  
    }
}
