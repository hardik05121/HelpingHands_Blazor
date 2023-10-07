using HelpingHands_DataAccess;

namespace HelpingHands_Business.Repository.IRepostiory
{
	public interface IApplicationUserRepository : IRepository<ApplicationUser>
    {
        Task<ApplicationUser> UpdateAsync(ApplicationUser entity);
    }
}