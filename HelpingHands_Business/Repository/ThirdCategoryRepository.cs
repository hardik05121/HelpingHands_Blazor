using HelpingHands_Business.Repository.IRepostiory;
using HelpingHands_DataAccess;
using HelpingHands_DataAccess.Data;

namespace HelpingHands_Business.Repository
{
    public class ThirdCategoryRepository : Repository<ThirdCategory>, IThirdCategoryRepository
    {
        private readonly ApplicationDbContext _db;
        public ThirdCategoryRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

  
        public async Task<ThirdCategory> UpdateAsync(ThirdCategory entity)
        {
         
            _db.ThirdCategories.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}
