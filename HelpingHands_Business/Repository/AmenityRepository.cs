using HelpingHands_Business.Repository.IRepostiory;
using HelpingHands_DataAccess;
using HelpingHands_DataAccess.Data;

namespace HelpingHands_Business.Repository
{
    public class AmenityRepository : Repository<Amenity>, IAmenityRepository
    {
        private readonly ApplicationDbContext _db;
        public AmenityRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

  
        public async Task<Amenity> UpdateAsync(Amenity entity)
        {
         
            _db.Amenities.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}
