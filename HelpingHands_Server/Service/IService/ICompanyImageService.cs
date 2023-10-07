
using HelpingHands_Models;
using HelpingHands_Models.ViewModels;

namespace HelpingHands_Server.Service.IService
{
    public interface ICompanyImageService
    {
        Task<T> GetAllAsync<T>();
        Task<T> GetAsync<T>(int id);
        Task<T> CreateAsync<T>(CompanyImageCreateVM dto);
        Task<T> SetAsync<T>(int imageId, int companyId);
        Task<T> DeleteAsync<T>(int id);

    }
}
