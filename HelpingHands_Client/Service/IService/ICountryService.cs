using HelpingHands_Models;
using HelpingHands_Models.ViewModels;

namespace HelpingHands_Client.Service.IService
{
    public interface ICountryService
    {
      
            Task<T> GetAllAsync<T>(string token);
            Task<T> GetAsync<T>(int id, string token);
            Task<T> CreateAsync<T>(CountryCreateDTO dto, string token);
            Task<T> UpdateAsync<T>(CountryUpdateDTO dto, string token);
            Task<T> DeleteAsync<T>(int id, string token);
        Task<T> CountryByLazyLoading<T>(int pageNum, string token);

        Task<T> CountryByPagination<T>(string term, string orderBy, int currentPage, string token);

    }
}
