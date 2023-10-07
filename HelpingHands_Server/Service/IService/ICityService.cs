
using HelpingHands_Models;

namespace HelpingHands_Server.Service.IService
{
    public interface ICityService
    {
            Task<T> GetAllAsync<T>();
            Task<T> GetAsync<T>(int id);
            Task<T> CreateAsync<T>(CityCreateDTO dto);
            Task<T> UpdateAsync<T>(CityUpdateDTO dt);
            Task<T> DeleteAsync<T>(int id);
        Task<T> CityByPagination<T>(string term, string orderBy, int currentPage);
    }
}
