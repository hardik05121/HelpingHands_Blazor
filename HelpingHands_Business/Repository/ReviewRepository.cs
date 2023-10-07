using HelpingHands_Business.Repository.IRepostiory;
using HelpingHands_DataAccess;
using HelpingHands_DataAccess.Data;

namespace HelpingHands_Business.Repository
{
    public class ReviewRepository : Repository<Review>, IReviewRepository
    {
        private readonly ApplicationDbContext _db;
        public ReviewRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

  
        public async Task<Review> UpdateAsync(Review entity)
        {
           
            _db.Reviews.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}
