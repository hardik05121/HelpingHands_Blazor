
using HelpingHands_Models;

namespace HelpingHands_Server.Service.IService
{
    public interface IApplicationUserRoleService
    {

        Task<T> GetAllAsync<T>();
        Task<T> GetAsync<T>(string Id);
    }
}
