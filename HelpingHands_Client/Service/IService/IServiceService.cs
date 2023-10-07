using HelpingHands_Models;
using HelpingHands_Models.ViewModels;

namespace HelpingHands_Client.Service.IService
{
    public interface IServiceService
    {
            Task<T> GetAllAsync<T>(string token);
            Task<T> GetAsync<T>(int id, string token);
            Task<T> CreateAsync<T>(ServiceCreateDTO dto, string token);
            Task<T> UpdateAsync<T>(ServiceUpdateDTO dto, string token);
            Task<T> DeleteAsync<T>(int id, string token);
        Task<T> ServiceByPagination<T>(string term, string orderBy, int currentPage, string token);
    }
}
