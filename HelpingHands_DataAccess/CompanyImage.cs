using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace HelpingHands_DataAccess
{
    public class CompanyImage
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [DisplayName("Company Image")]
        public string Image { get; set; }

        [ForeignKey("Company")]
        public int CompanyId { get; set; }
        [ValidateNever]
        public Company Company { get; set; }

        public bool IsActive { get; set; }

    }
}
