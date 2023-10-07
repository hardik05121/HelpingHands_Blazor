using HelpingHands_Business.Repository.IRepostiory;
using HelpingHands_DataAccess;
using HelpingHands_DataAccess.Data;

namespace HelpingHands_Business.Repository
{
    public class ReviewXCommentRepository : Repository<ReviewXComment>, IReviewXCommentRepository
    {
        private readonly ApplicationDbContext _db;
        public ReviewXCommentRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

  
        public async Task<ReviewXComment> UpdateAsync(ReviewXComment entity)
        {

             
            _db.ReviewXComments.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}
