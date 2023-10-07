using HelpingHands_DataAccess;

namespace HelpingHands_Business.Repository.IRepostiory
{
    public interface ICompanyXAmenityRepository : IRepository<CompanyXAmenity>
    {
      
        Task<CompanyXAmenity> UpdateAsync(CompanyXAmenity entity);

    }
}
