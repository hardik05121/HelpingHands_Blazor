
using HelpingHands_Models;
namespace HelpingHands_Client.Service.IService
{
	public interface IApplicationRoleService
    {
            Task<T> GetAllAsync<T>();
            Task<T> GetAsync<T>(string Id);
            Task<T> CreateAsync<T>(ApplicationRoleDTO dto);
            Task<T> UpdateAsync<T>(ApplicationRoleDTO dto);
            Task<T> DeleteAsync<T>(string id);
        Task<T> ApplicationRoleByPagination<T>(string term, string orderBy, int currentPage);
    }
}
