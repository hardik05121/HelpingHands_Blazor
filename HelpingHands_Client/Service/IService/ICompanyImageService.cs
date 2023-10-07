using HelpingHands_Models;
using HelpingHands_Models.ViewModels;

namespace HelpingHands_Client.Service.IService
{
    public interface ICompanyImageService
    {
        Task<T> GetAllAsync<T>(string token);
        Task<T> GetAsync<T>(int id, string token);
        Task<T> CreateAsync<T>(CompanyImageCreateVM dto, string token);
        Task<T> SetAsync<T>(int imageId, int companyId, string token);
        Task<T> DeleteAsync<T>(int id, string token);

    }
}
