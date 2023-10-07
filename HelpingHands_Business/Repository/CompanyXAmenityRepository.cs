using HelpingHands_Business.Repository.IRepostiory;
using HelpingHands_DataAccess;
using HelpingHands_DataAccess.Data;

namespace HelpingHands_Business.Repository
{
    public class CompanyXAmenityRepository : Repository<CompanyXAmenity>, ICompanyXAmenityRepository
    {
        private readonly ApplicationDbContext _db;
        public CompanyXAmenityRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

  
        public async Task<CompanyXAmenity> UpdateAsync(CompanyXAmenity entity)
        {
         
            _db.CompanyXAmenities.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }

    }
}
