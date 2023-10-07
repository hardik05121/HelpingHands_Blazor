using HelpingHands_Business.Repository.IRepostiory;
using HelpingHands_DataAccess;
using HelpingHands_DataAccess.Data;

namespace HelpingHands_Business.Repository
{
	public class ApplicationRoleRepository : Repository<ApplicationRole>, IApplicationRoleRepository
	{
        private readonly ApplicationDbContext _db;
        public ApplicationRoleRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<ApplicationRole> UpdateAsync(ApplicationRole entity)
        {
            _db.ApplicationRoles.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}
