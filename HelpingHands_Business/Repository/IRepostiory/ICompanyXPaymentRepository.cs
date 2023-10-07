using HelpingHands_DataAccess;

namespace HelpingHands_Business.Repository.IRepostiory
{
    public interface ICompanyXPaymentRepository : IRepository<CompanyXPayment>
    {
      
        Task<CompanyXPayment> UpdateAsync(CompanyXPayment entity);
  
    }
}
