using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HelpingHands_Models
{
    public class StateDTO
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [DisplayName("State Name")]
        [StringLength(15, MinimumLength = 3, ErrorMessage = "The field must be between 3 and 15 characters.")]
        public string StateName { get; set; }
        public int CountryId { get; set; }
        [ValidateNever]
        public CountryDTO Country { get; set; }
        public bool IsActive { get; set; }


    }
}
