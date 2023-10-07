using HelpingHands_Business.Repository.IRepostiory;
using HelpingHands_DataAccess;
using HelpingHands_DataAccess.Data;

namespace HelpingHands_Business.Repository
{
    public class FirstCategoryRepository : Repository<FirstCategory>, IFirstCategoryRepository
    {
        private readonly ApplicationDbContext _db;
        public FirstCategoryRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<FirstCategory> UpdateAsync(FirstCategory entity)
        {
            _db.FirstCategories.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}
