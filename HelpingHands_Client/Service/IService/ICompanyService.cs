
using HelpingHands_Models;

namespace HelpingHands_Client.Service.IService
{
    public interface ICompanyService
    {
        Task<T> GetAllAsync<T>();
        Task<T> GetAsync<T>(int id);
        Task<T> CreateAsync<T>(CompanyDTO dto);
        Task<T> UpdateAsync<T>(CompanyDTO dto);
        Task<T> DeleteAsync<T>(int id);
        Task<T> CompanySearchByLazyLoading<T>(int pageNum, string searchText);
        Task<T> CompanyByPagination<T>(string term, string orderBy, int currentPage);
    }
}
