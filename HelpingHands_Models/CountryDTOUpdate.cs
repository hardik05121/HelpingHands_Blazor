using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HelpingHands_Models
{
    public class CountryUpdateDTO
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [DisplayName("country name")]
        [StringLength(15, MinimumLength = 3, ErrorMessage = "The field must be between 3 and 15 characters.")]
        public string CountryName { get; set; }

        public bool IsActive { get; set; }


    }
}
