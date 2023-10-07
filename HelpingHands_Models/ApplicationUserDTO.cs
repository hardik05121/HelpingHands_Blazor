using Microsoft.AspNetCore.Identity;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HelpingHands_Models
{
    public class ApplicationUserDTO : IdentityUser
    {
        public string FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Address { get; set; }
        public string PassWord { get; set; }
        public string Role { get; set; }
    }

}
