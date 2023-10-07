using HelpingHands_DataAccess;

namespace HelpingHands_Business.Repository.IRepostiory
{
	public interface IApplicationUserRoleRepository : IRepository<ApplicationUserRole>
    {
        Task<ApplicationUserRole> UpdateAsync(ApplicationUserRole entity);
    }
}