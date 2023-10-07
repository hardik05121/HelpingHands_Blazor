using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HelpingHands_DataAccess
{
    public class ReviewXComment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
 
        [ForeignKey("Company")]
        public int CompanyId { get; set; }
        [ValidateNever]
        public Company Company { get; set; }
        
        [ForeignKey("Review")]
        public int ReviewId { get; set; }
        [ValidateNever]
        public Review Review { get; set; }

        [ForeignKey("ApplicationUser")]
        public string ApplicationUserId { get; set; }
        [ValidateNever]
        public ApplicationUser ApplicationUser { get; set; }

        [DisplayName("Created Date")]
        public DateTime CreatedDate { get; set; }

        [Required]
        [DisplayName("Replay Comment")]
        public string Comment { get; set; }

    }
}
