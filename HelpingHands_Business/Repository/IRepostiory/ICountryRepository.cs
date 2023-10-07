using HelpingHands_DataAccess;

namespace HelpingHands_Business.Repository.IRepostiory
{
    public interface ICountryRepository : IRepository<Country>
    {
      
        Task<Country> UpdateAsync(Country entity);
  
    }
}
