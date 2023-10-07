using HelpingHands_Models;
using HelpingHands_Models.ViewModels;

namespace HelpingHands_Client.Service.IService
{
    public interface IPaymentService
    {
      
            Task<T> GetAllAsync<T>(string token);
            Task<T> GetAsync<T>(int id, string token);
            Task<T> CreateAsync<T>(PaymentCreateDTO dto, string token);
            Task<T> UpdateAsync<T>(PaymentUpdateDTO dto, string token);
            Task<T> DeleteAsync<T>(int id, string token);
        Task<T> PaymentByPagination<T>(string term, string orderBy, int currentPage, string token);
    }
}
