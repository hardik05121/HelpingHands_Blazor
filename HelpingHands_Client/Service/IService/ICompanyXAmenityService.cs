
using HelpingHands_Models;
using HelpingHands_Models.ViewModels;

namespace HelpingHands_Client.Service.IService
{
    public interface ICompanyXAmenityService

    {

        Task<T> GetAllAsync<T>();
        Task<T> GetAsync<T>(int id);
        Task<T> CreateAsync<T>(CompanyXAmenityVM dt);
        //Task<T> UpdateAsync<T>(CompanyXAmenityVM dto, string token);
        Task<T> DeleteAsync<T>(int id);


    }
}
