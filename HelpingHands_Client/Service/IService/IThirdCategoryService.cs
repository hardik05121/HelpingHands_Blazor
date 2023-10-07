using HelpingHands_Models;
using HelpingHands_Models.ViewModels;

namespace HelpingHands_Client.Service.IService
{
    public interface IThirdCategoryService
    {
      
            Task<T> GetAllAsync<T>(string token);
            Task<T> GetAsync<T>(int id, string token);
            Task<T> CreateAsync<T>(ThirdCategoryCreateDTO dto, string token);
            Task<T> UpdateAsync<T>(ThirdCategoryUpdateDTO dto, string token);
            Task<T> DeleteAsync<T>(int id, string token);
        Task<T> ThirdCategoryByPagination<T>(string term, string orderBy, int currentPage, string token);
    }
}
