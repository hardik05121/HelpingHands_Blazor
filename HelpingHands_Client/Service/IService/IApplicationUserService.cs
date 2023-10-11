
using HelpingHands_Models;
using HelpingHands_Models.ViewModels;

namespace HelpingHands_Client.Service.IService
{
	public interface IApplicationUserService
    {
            Task<T> GetAllAsync<T>();
            Task<T> GetAsync<T>(string Id);
            Task<T> UpdateAsync<T>(UserVM dto);
        Task<T> ApplicationUserByPagination<T>(string term, string orderBy, int currentPage);
    }
}
