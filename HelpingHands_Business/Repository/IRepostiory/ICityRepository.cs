using HelpingHands_DataAccess;

namespace HelpingHands_Business.Repository.IRepostiory
{
    public interface ICityRepository : IRepository<City>
    {
      
        Task<City> UpdateAsync(City entity);
  
    }
}
