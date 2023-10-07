using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HelpingHands_Models
{
	public class LoginResponseDTO
    {
        public ApplicationUserDTO User { get; set; }
        public string Token { get; set; }
    }
}
