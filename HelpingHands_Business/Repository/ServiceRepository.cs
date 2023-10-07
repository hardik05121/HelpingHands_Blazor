using HelpingHands_Business.Repository.IRepostiory;
using HelpingHands_DataAccess;
using HelpingHands_DataAccess.Data;

namespace HelpingHands_Business.Repository
{
    public class ServiceRepository : Repository<Service>, IServiceRepository
    {
        private readonly ApplicationDbContext _db;
        public ServiceRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

  
        public async Task<Service> UpdateAsync(Service entity)
        {
         
            _db.Services.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}
