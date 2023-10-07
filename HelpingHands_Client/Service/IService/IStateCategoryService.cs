using HelpingHands_Models;
using HelpingHands_Models.ViewModels;

namespace HelpingHands_Client.Service.IService
{
    public interface IStateService
    {

        Task<T> GetAllAsync<T>(string token);
        Task<T> GetAsync<T>(int id, string token);
        Task<T> CreateAsync<T>(StateCreateDTO dto, string token);
        Task<T> UpdateAsync<T>(StateUpdateDTO dto, string token);
        Task<T> DeleteAsync<T>(int id, string token);
        Task<T> StateByPagination<T>(string term, string orderBy, int currentPage, string token);
    }
}
