using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HelpingHands_Models
{
    public class CompanyImageUpdateDTO
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [DisplayName("Company Image")]
        public string Image { get; set; }
        public int CompanyId { get; set; }
        public bool IsActive { get; set; }


    }
}
