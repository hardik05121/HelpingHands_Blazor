using HelpingHands_Models;
using HelpingHands_Models.ViewModels;

namespace HelpingHands_Client.Service.IService
{
    public interface ICompanyService

    {

        Task<T> GetAllAsync<T>(string token);
        Task<T> GetAsync<T>(int id, string token);
        Task<T> CreateAsync<T>(CompanyDTO dto, string token);
        Task<T> UpdateAsync<T>(CompanyDTO dto, string token);
        Task<T> DeleteAsync<T>(int id, string token);
        Task<T> CompanySearchByLazyLoading<T>(int pageNum, string searchText,string token);
        Task<T> CompanyByPagination<T>(string term, string orderBy, int currentPage, string token);
    }
}
