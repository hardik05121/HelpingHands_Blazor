using HelpingHands_DataAccess;

namespace HelpingHands_Business.Repository.IRepostiory
{
    public interface IServiceRepository : IRepository<Service>
    {
      
        Task<Service> UpdateAsync(Service entity);
  
    }
}
