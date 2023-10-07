using Microsoft.AspNetCore.Identity;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HelpingHands_Models
{
    public class ApplicationRoleDTO : IdentityRole
    {
        public bool IsChecked { get; set; }
    }
}
