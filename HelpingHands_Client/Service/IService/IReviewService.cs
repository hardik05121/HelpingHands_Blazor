using HelpingHands_Models;
using HelpingHands_Models.ViewModels;

namespace HelpingHands_Client.Service.IService
{
    public interface IReviewService
    {
        Task<T> GetAllAsync<T>(string token);
        Task<T> GetAsync<T>(int id, string token);
        Task<T> CreateAsync<T>(ReviewCreateVM dto, string token);
        Task<T> UpdateAsync<T>(ReviewUpdateDTO dto, string token);
        Task<T> DeleteAsync<T>(int id, string token);
        Task<T> ReviewByPagination<T>(string term, string orderBy, int currentPage, string token);
    }
}
