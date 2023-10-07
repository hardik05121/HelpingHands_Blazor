using HelpingHands_Business.Repository.IRepostiory;
using HelpingHands_DataAccess;
using HelpingHands_DataAccess.Data;

namespace HelpingHands_Business.Repository
{
    public class CompanyXServiceRepository : Repository<CompanyXService>, ICompanyXServiceRepository
    {
        private readonly ApplicationDbContext _db;
        public CompanyXServiceRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

  
        public async Task<CompanyXService> UpdateAsync(CompanyXService entity)
        {
         
            _db.CompanyXServices.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}
