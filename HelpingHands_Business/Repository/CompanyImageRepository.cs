using HelpingHands_Business.Repository.IRepostiory;
using HelpingHands_DataAccess;
using HelpingHands_DataAccess.Data;

namespace HelpingHands_Business.Repository
{
    public class CompanyImageRepository : Repository<CompanyImage>, ICompanyImageRepository
    {
        private readonly ApplicationDbContext _db;
        public CompanyImageRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

  
        public async Task<CompanyImage> UpdateAsync(CompanyImage entity)
        {
            _db.CompanyImages.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}
