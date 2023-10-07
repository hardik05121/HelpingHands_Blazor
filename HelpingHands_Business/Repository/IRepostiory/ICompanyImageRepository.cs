using HelpingHands_DataAccess;

namespace HelpingHands_Business.Repository.IRepostiory
{
    public interface ICompanyImageRepository : IRepository<CompanyImage>
    {
      
        Task<CompanyImage> UpdateAsync(CompanyImage entity);
  
    }
}
