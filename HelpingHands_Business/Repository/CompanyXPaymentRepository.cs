using HelpingHands_Business.Repository.IRepostiory;
using HelpingHands_DataAccess;
using HelpingHands_DataAccess.Data;

namespace HelpingHands_Business.Repository
{
    public class CompanyXPaymentRepository : Repository<CompanyXPayment>, ICompanyXPaymentRepository
    {
        private readonly ApplicationDbContext _db;
        public CompanyXPaymentRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

  
        public async Task<CompanyXPayment> UpdateAsync(CompanyXPayment entity)
        {
         
            _db.CompanyXPayments.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}
