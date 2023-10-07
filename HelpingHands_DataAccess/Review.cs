using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HelpingHands_DataAccess
{
    public class Review
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("Company")]
        public int CompanyId { get; set; }
        [ValidateNever]
        public Company Company { get; set; }

        [ForeignKey("ApplicationUser")]
        public string ApplicationUserId { get; set; }
        [ValidateNever]
        public ApplicationUser ApplicationUser { get; set; }


        [DisplayName("Created Date")]
        public DateTime CreatedDate { get; set; }



        [DisplayName("Like Count")]
        public int? LikeCount { get; set; }

  
        [DisplayName("Dis Like Count")]
        public int? DisLikeCount { get; set; }

        [DisplayName("View Count")]
        public int? ViewCount { get; set; }

        [DisplayName("Rating")]
        public int Rating { get; set; }

        [Required]
        [DisplayName("Review Title")]
        public string Title { get; set; }

        [Required]
        [DisplayName("Review in Description")]
        public string Description { get; set; }

  


    }
}
