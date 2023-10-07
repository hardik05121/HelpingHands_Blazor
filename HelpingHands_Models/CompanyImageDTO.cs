using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HelpingHands_Models
{
    public class CompanyImageDTO
    {
        [Required]
        public int Id { get; set; }

        [DisplayName("Company Image")]
        public string Image { get; set; }
        public int CompanyId { get; set; }
        [ValidateNever]
        public CompanyDTO Company { get; set; }
        public bool IsActive { get; set; }
    }
}
