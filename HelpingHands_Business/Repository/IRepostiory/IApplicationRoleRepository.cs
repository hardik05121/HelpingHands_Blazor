using HelpingHands_DataAccess;

namespace HelpingHands_Business.Repository.IRepostiory
{
	public interface IApplicationRoleRepository : IRepository<ApplicationRole>
    {
        Task<ApplicationRole> UpdateAsync(ApplicationRole entity);
    }
}

