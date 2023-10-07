using HelpingHands_DataAccess;

namespace HelpingHands_Business.Repository.IRepostiory
{
    public interface IAmenityRepository : IRepository<Amenity>
    {
        Task<Amenity> UpdateAsync(Amenity entity);
    }
}
