using HelpingHands_Business.Repository.IRepostiory;
using HelpingHands_DataAccess;
using HelpingHands_DataAccess.Data;

namespace HelpingHands_Business.Repository
{
    public class CompanyRepository : Repository<Company>, ICompanyRepository
    {
        private readonly ApplicationDbContext _db;
        public CompanyRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

  
        public async Task<Company> UpdateAsync(Company entity)
        {
            entity.UpdatedDate = System.DateTime.Now;
            _db.Companies.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}
