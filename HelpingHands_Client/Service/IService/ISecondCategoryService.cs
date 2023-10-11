
using HelpingHands_Models;

namespace HelpingHands_Client.Service.IService
{
    public interface ISecondCategoryService
    {
      
            Task<T> GetAllAsync<T>();
            Task<T> GetSecondCategoryByFirstCategory<T>(int id);
            Task<T> GetAsync<T>(int id);
            Task<T> CreateAsync<T>(SecondCategoryCreateDTO dto);
            Task<T> UpdateAsync<T>(SecondCategoryUpdateDTO dto);
            Task<T> DeleteAsync<T>(int id);
        Task<T> SecondCategoryByPagination<T>(string term, string orderBy, int currentPage);
    }
}
