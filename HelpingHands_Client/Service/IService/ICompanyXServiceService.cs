
using HelpingHands_Models;
using HelpingHands_Models.ViewModels;

namespace HelpingHands_Client.Service.IService
{
    public interface ICompanyXServiceService
	{
        Task<T> GetAllAsync<T>();
        Task<T> GetAsync<T>(int id);
        Task<T> CreateAsync<T>(CompanyXServiceVM dto);
      //  Task<T> UpdateAsync<T>(CompanyXServiceUpdateVM dto, string token);
        Task<T> DeleteAsync<T>(int id);
    }
}
