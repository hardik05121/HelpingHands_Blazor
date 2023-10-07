using HelpingHands_Models;
using HelpingHands_Models.ViewModels;

namespace HelpingHands_Client.Service.IService
{
    public interface IEnquiryService
    {

        Task<T> GetAllAsync<T>(string token);
        Task<T> GetAsync<T>(int id, string token);
        Task<T> CreateAsync<T>(EnquiryDTO dto, string token);
        Task<T> DeleteAsync<T>(int id, string token);
        Task<T> EnquiryByPagination<T>(string term, string orderBy, int currentPage, string token);
    }
}
