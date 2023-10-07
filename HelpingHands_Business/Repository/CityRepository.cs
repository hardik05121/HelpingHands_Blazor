using HelpingHands_Business.Repository.IRepostiory;
using HelpingHands_DataAccess;
using HelpingHands_DataAccess.Data;

namespace HelpingHands_Business.Repository
{
    public class CityRepository : Repository<City>, ICityRepository
    {
        private readonly ApplicationDbContext _db;
        public CityRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

  
        public async Task<City> UpdateAsync(City entity)
        {
         
            _db.Cities.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}
