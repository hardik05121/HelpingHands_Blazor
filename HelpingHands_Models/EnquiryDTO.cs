using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HelpingHands_Models
{
    public class EnquiryDTO
    {
       [Required]
        public int Id { get; set; }

        public int CompanyID { get; set; }
        [ValidateNever]
        public CompanyDTO Company { get; set; }

        [Required]
        [DisplayName("User Name")]
        public string UserName { get; set; }

        [Required]
        [DisplayName("User Email")]
		[StringLength(20)]
		[DataType(DataType.EmailAddress)]
		[RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[a-z]{2,4}", ErrorMessage = "Invalid Email Address")]
		public string Email { get; set; }

        [Required]
        [DisplayName("User Phone Number")]
        [RegularExpression(@"^\d{9}$", ErrorMessage = "The phone number must be exactly 9 digits.")]
        public int PhoneNumber { get; set; }

        [Required]
        [DisplayName("Enquiry Title")]
        public string Title { get; set; }

        [Required]
        [DisplayName("Brif Description Of Your Enquiry")]
        public string Description { get; set; }
    }
}
