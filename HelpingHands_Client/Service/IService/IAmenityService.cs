
using HelpingHands_Models;

namespace HelpingHands_Client.Service.IService
{
    public interface IAmenityService
    {

        Task<T> GetAllAsync<T>(string token);
        Task<T> GetAsync<T>(int id, string token);
        Task<T> CreateAsync<T>(AmenityCreateDTO dto, string token);
        Task<T> UpdateAsync<T>(AmenityUpdateDTO dto, string token);
        Task<T> DeleteAsync<T>(int id, string token);
        Task<T> AmenityByPagination<T>(string term, string orderBy, int currentPage, string token);
    }
}
