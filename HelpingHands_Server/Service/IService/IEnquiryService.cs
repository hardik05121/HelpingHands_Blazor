
using HelpingHands_Models;

namespace HelpingHands_Server.Service.IService
{
    public interface IEnquiryService
    {

        Task<T> GetAllAsync<T>();
        Task<T> GetAsync<T>(int id);
        Task<T> CreateAsync<T>(EnquiryDTO dto);
        Task<T> DeleteAsync<T>(int id);
        Task<T> EnquiryByPagination<T>(string term, string orderBy, int currentPage);
    }
}
