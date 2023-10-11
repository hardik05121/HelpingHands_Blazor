
using HelpingHands_Models;

namespace HelpingHands_Client.Service.IService
{
	public interface IAuthService
    {
        Task<T> LoginAsync<T>(LoginRequestDTO objToCreate);
        Task<T> RegisterAsync<T>(RegisterationRequestDTO objToCreate);
    }
}
