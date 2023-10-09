
using HelpingHands_Models;

namespace HelpingHands_Server.Service.IService
{
    public interface IPaymentService
    {
      
            Task<T> GetAllAsync<T>();
            Task<T> GetAsync<T>(int id);
            Task<T> CreateAsync<T>(PaymentCreateDTO dto);
            Task<T> UpdateAsync<T>(PaymentUpdateDTO dto);
            Task<T> DeleteAsync<T>(int id);
            Task<T> PaymentByPagination<T>(string term, string orderBy, int currentPage);
    }
}
