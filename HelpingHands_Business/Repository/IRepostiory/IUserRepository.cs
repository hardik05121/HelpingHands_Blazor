using HelpingHands_Models;

namespace HelpingHands_Business.Repository.IRepostiory
{
	public interface IUserRepository
    {
        bool IsUniqueUser(string username);
        Task<LoginResponseDTO> Login(LoginRequestDTO loginRequestDTO);
        Task<ApplicationUserDTO> Register(RegisterationRequestDTO registerationRequestDTO);
    }
}
