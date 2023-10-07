using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HelpingHands_DataAccess
{
    public class ThirdCategory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [DisplayName("Third Category")]
        public string ThirdCategoryName { get; set; }

        [DisplayName("Third Category Image")]
        public string ThirdCategoryImage { get; set; }

        [ForeignKey("FirstCategory")]
        public int FirstCategoryId { get; set; }
        [ValidateNever]
        public FirstCategory FirstCategory { get; set; }

        [ForeignKey("SecondCategory")]
        public int SecondCategoryId { get; set; }
        [ValidateNever]
        public SecondCategory SecondCategory { get; set; }

        public bool IsActive { get; set; }
    }
}
