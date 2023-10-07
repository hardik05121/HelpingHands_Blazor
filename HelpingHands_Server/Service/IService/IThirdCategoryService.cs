
using HelpingHands_Models;

namespace HelpingHands_Server.Service.IService
{
    public interface IThirdCategoryService
    {
      
            Task<T> GetAllAsync<T>();
            Task<T> GetAsync<T>(int id);
            Task<T> CreateAsync<T>(ThirdCategoryCreateDTO dto);
            Task<T> UpdateAsync<T>(ThirdCategoryUpdateDTO dto);
            Task<T> DeleteAsync<T>(int id);
        Task<T> ThirdCategoryByPagination<T>(string term, string orderBy, int currentPage);
    }
}
