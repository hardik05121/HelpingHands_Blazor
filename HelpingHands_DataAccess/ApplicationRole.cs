using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace HelpingHands_DataAccess
{
    public class ApplicationRole : IdentityRole
    {
        [NotMapped]
        public bool IsChecked { get; set; }
    }

}
