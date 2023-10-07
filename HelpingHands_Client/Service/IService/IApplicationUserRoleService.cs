using HelpingHands_Models;

namespace HelpingHands_Client.Service.IService
{
    public interface IApplicationUserRoleService
    {

        Task<T> GetAllAsync<T>(string token);
        Task<T> GetAsync<T>(string Id, string token);
    }
}
