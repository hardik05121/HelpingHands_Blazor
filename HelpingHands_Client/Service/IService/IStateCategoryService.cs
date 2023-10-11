
using HelpingHands_Models;

namespace HelpingHands_Client.Service.IService
{
    public interface IStateService
    {

        Task<T> GetAllAsync<T>();
        Task<T> GetAsync<T>(int id);
        Task<T> CreateAsync<T>(StateCreateDTO dto);
        Task<T> UpdateAsync<T>(StateUpdateDTO dto);
        Task<T> DeleteAsync<T>(int id);
        Task<T> StateByPagination<T>(string term, string orderBy, int currentPage);
    }
}
