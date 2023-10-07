using HelpingHands_DataAccess;

namespace HelpingHands_Business.Repository.IRepostiory
{
    public interface IStateRepository : IRepository<State>
    {
      
        Task<State> UpdateAsync(State entity);
  
    }
}
