using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HelpingHands_Models
{
    public class FirstCategoryDTO
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [DisplayName("First Category")]
        [StringLength(15, MinimumLength = 3, ErrorMessage = "The field must be between 3 and 15 characters.")]
        public string FirstCategoryName { get; set; }
        [DisplayName("First CategoryImage")]
        public string FirstCategoryImage { get; set; }

        public bool IsActive { get; set; }


    }
}
