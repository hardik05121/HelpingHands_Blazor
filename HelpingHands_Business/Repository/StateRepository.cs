using HelpingHands_Business.Repository.IRepostiory;
using HelpingHands_DataAccess;
using HelpingHands_DataAccess.Data;

namespace HelpingHands_Business.Repository
{
    public class StateRepository : Repository<State>, IStateRepository
    {
        private readonly ApplicationDbContext _db;
        public StateRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

  
        public async Task<State> UpdateAsync(State entity)
        {
         
            _db.States.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}
