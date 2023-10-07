using HelpingHands_Models;
using HelpingHands_Models.ViewModels;

namespace HelpingHands_Client.Service.IService
{
    public interface ICompanyXServiceService

	{

        Task<T> GetAllAsync<T>(string token);
        Task<T> GetAsync<T>(int id, string token);
        Task<T> CreateAsync<T>(CompanyXServiceVM dto, string token);
      //  Task<T> UpdateAsync<T>(CompanyXServiceUpdateVM dto, string token);
        Task<T> DeleteAsync<T>(int id, string token);


    }
}
