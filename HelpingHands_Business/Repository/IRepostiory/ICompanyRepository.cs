using HelpingHands_DataAccess;

namespace HelpingHands_Business.Repository.IRepostiory
{
    public interface ICompanyRepository : IRepository<Company>
    {
      
        Task<Company> UpdateAsync(Company entity);
  
    }
}
