using HelpingHands_Models;
using HelpingHands_Models.ViewModels;

namespace HelpingHands_Client.Service.IService
{
    public interface ISecondCategoryService
    {
      
            Task<T> GetAllAsync<T>(string token);
            Task<T> GetSecondCategoryByFirstCategory<T>(int id,string token);
            Task<T> GetAsync<T>(int id, string token);
            Task<T> CreateAsync<T>(SecondCategoryCreateDTO dto, string token);
            Task<T> UpdateAsync<T>(SecondCategoryUpdateDTO dto, string token);
            Task<T> DeleteAsync<T>(int id, string token);
        Task<T> SecondCategoryByPagination<T>(string term, string orderBy, int currentPage, string token);
    }
}
