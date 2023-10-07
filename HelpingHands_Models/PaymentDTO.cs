using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HelpingHands_Models
{
    public class PaymentDTO
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [DisplayName("Payment Name")]
        [StringLength(15, MinimumLength = 3, ErrorMessage = "The field must be between 3 and 15 characters.")]
        public string PaymentName { get; set; }

        public bool IsActive { get; set; }


    }
}
