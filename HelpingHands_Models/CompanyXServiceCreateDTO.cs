using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HelpingHands_Models
{
    public class CompanyXServiceCreateDTO
    {
        [Required]
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public int ServiceId{ get; set; }

    }
}
