using HelpingHands_Business.Repository.IRepostiory;
using HelpingHands_DataAccess;
using HelpingHands_DataAccess.Data;

namespace HelpingHands_Business.Repository
{
    public class SecondCategoryRepository : Repository<SecondCategory>, ISecondCategoryRepository
    {
        private readonly ApplicationDbContext _db;
        public SecondCategoryRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

  
        public async Task<SecondCategory> UpdateAsync(SecondCategory entity)
        {
         
            _db.SecondCategories.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}
