
using HelpingHands_Models;
using HelpingHands_Models.ViewModels;

namespace HelpingHands_Server.Service.IService
{
    public interface ICompanyXPaymentService
    {

        Task<T> GetAllAsync<T>();
        Task<T> GetAsync<T>(int id);
        Task<T> CreateAsync<T>(CompanyXPaymentVM dto);
        Task<T> UpdateAsync<T>(CompanyXPaymentVM dto);
        Task<T> DeleteAsync<T>(int id);

    }
}
