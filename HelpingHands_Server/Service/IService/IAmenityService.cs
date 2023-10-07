
using HelpingHands_Models;

namespace HelpingHands_Server.Service.IService
{
    public interface IAmenityService
    {
        Task<T> GetAllAsync<T>();
        Task<T> GetAsync<T>(int id);
        Task<T> CreateAsync<T>(AmenityCreateDTO dto);
        Task<T> UpdateAsync<T>(AmenityUpdateDTO dto);
        Task<T> DeleteAsync<T>(int id);
        Task<T> AmenityByPagination<T>(string term, string orderBy, int currentPage);
    }
}
