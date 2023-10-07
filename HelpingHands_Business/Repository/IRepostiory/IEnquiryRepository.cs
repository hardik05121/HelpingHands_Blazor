using HelpingHands_DataAccess;

namespace HelpingHands_Business.Repository.IRepostiory
{
    public interface IEnquiryRepository : IRepository<Enquiry>
    {
      
        Task<Enquiry> UpdateAsync(Enquiry entity);
  
    }
}
