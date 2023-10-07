using HelpingHands_Business.Repository.IRepostiory;
using HelpingHands_DataAccess;
using HelpingHands_DataAccess.Data;

namespace HelpingHands_Business.Repository
{
    public class EnquiryRepository : Repository<Enquiry>, IEnquiryRepository
    {
        private readonly ApplicationDbContext _db;
        public EnquiryRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

  
        public async Task<Enquiry> UpdateAsync(Enquiry entity)
        {
         
            _db.Enquiries.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}
