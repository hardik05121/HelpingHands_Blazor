
using HelpingHands_Models;
using HelpingHands_Models.ViewModels;

namespace HelpingHands_Client.Service.IService
{
    public interface IReviewService
    {
        Task<T> GetAllAsync<T>();
        Task<T> GetAsync<T>(int id);
        Task<T> CreateAsync<T>(ReviewCreateVM dto);
        Task<T> UpdateAsync<T>(ReviewUpdateDTO dto);
        Task<T> DeleteAsync<T>(int id);
        Task<T> ReviewByPagination<T>(string term, string orderBy, int currentPage);
    }
}
