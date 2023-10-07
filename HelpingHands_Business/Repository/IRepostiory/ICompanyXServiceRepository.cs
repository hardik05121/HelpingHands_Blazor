using HelpingHands_DataAccess;

namespace HelpingHands_Business.Repository.IRepostiory
{
    public interface ICompanyXServiceRepository : IRepository<CompanyXService>
    {
      
        Task<CompanyXService> UpdateAsync(CompanyXService entity);
  
    }
}
