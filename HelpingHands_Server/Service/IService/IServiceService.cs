
using HelpingHands_Models;

namespace HelpingHands_Server.Service.IService
{
    public interface IServiceService
    {
            Task<T> GetAllAsync<T>();
            Task<T> GetAsync<T>(int id);
            Task<T> CreateAsync<T>(ServiceCreateDTO dto);
            Task<T> UpdateAsync<T>(ServiceUpdateDTO dto);
            Task<T> DeleteAsync<T>(int id);
        Task<T> ServiceByPagination<T>(string term, string orderBy, int currentPage);
    }
}
