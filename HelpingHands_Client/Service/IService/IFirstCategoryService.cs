
using HelpingHands_Models;

namespace HelpingHands_Client.Service.IService
{
    public interface IFirstCategoryService
    {
      
            Task<T> GetAllAsync<T>();
            Task<T> GetAsync<T>(int id);
            Task<T> CreateAsync<T>(FirstCategoryCreateDTO dto);
            Task<T> UpdateAsync<T>(FirstCategoryUpdateDTO dto);
            Task<T> DeleteAsync<T>(int id);
        Task<T> FirstCategoryByPagination<T>(string term, string orderBy, int currentPage);
    }
}
