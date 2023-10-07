using HelpingHands_DataAccess;

namespace HelpingHands_Business.Repository.IRepostiory
{
    public interface IPaymentRepository : IRepository<Payment>
    {
      
        Task<Payment> UpdateAsync(Payment entity);
  
    }
}
