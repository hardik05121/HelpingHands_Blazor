using HelpingHands_Models;

namespace HelpingHands_Client.Service.IService
{
	public interface IApplicationRoleService
    {
            Task<T> GetAllAsync<T>(string token);
            Task<T> GetAsync<T>(string Id, string token);
            Task<T> CreateAsync<T>(ApplicationRoleDTO dto, string token);
            Task<T> UpdateAsync<T>(ApplicationRoleDTO dto, string token);
            Task<T> DeleteAsync<T>(string id, string token);
        Task<T> ApplicationRoleByPagination<T>(string term, string orderBy, int currentPage, string token);
    }
}
