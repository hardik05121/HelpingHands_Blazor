using HelpingHands_Business.Repository.IRepostiory;
using HelpingHands_DataAccess;
using HelpingHands_DataAccess.Data;

namespace HelpingHands_Business.Repository
{
    public class PaymentRepository : Repository<Payment>, IPaymentRepository
    {
        private readonly ApplicationDbContext _db;
        public PaymentRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

  
        public async Task<Payment> UpdateAsync(Payment entity)
        {
         
            _db.Payments.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}
