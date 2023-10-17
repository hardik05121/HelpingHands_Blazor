using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HelpingHands_Models
{
    public class RegisterationRequestDTO
    {
        [Required]
        [RegularExpression(@"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$", ErrorMessage = "Invalid Password Enter 8 character password like 'Example@123' ")]
        [DisplayName("password")]
        public string Password { get; set; }
        [Required]
        [Compare("Password", ErrorMessage = "The password not match.")]
        [DisplayName("confirm password")]
        public string ConfirmPassword { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? PostalCode { get; set; }
        [Required]
        [StringLength(13, MinimumLength = 9, ErrorMessage = "The field must be between 9 and 13 characters.")]
        [DisplayName("phonenumber")]
        public string PhoneNumber { get; set; }
        [Required]
        [StringLength(60, MinimumLength = 3)]
        [DisplayName("firstname")]
        public string FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }

        [DisplayName("address")]
        public string? Address { get; set; }
        public string? Country { get; set; }
        [Required]
        [StringLength(50)]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[a-z]{2,4}", ErrorMessage = "Invalid Email Address Enter")]
        [DisplayName("email")]
        public string Email { get; set; }
    }
}
