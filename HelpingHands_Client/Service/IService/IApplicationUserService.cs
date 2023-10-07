using HelpingHands_Models;
using HelpingHands_Models.ViewModels;

namespace HelpingHands_Client.Service.IService
{
	public interface IApplicationUserService
    {
            Task<T> GetAllAsync<T>(string token);
            Task<T> GetAsync<T>(string Id, string token);
            Task<T> UpdateAsync<T>(UserVM dto, string token);
        Task<T> ApplicationUserByPagination<T>(string term, string orderBy, int currentPage, string token);
    }
}
