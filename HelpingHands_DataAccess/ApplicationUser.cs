using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace HelpingHands_DataAccess
{
    public class ApplicationUser :IdentityUser
    {
        public string FirstName { get; set; }
        public string? LastName { get; set; }
		public string? Address { get; set; }
        public string PassWord { get; set; }
        [NotMapped]
        public string Role { get; set; }    
    }

}
