using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HelpingHands_Models
{
    public class CompanyDTO
    {

        public int Id { get; set; }

        [Required]
        [DisplayName("company name")]
        [StringLength(60, MinimumLength = 3, ErrorMessage = "The field must be between 3 and 60 characters.")]
        public string CompanyName { get; set; }

        [DisplayName("company logo")]
        public string? CompanyLogo { get; set; }

        [Required]
        [DisplayName("firstcategory name")]
        public int FirstCategoryId { get; set; }
        [ValidateNever]
        public FirstCategoryDTO FirstCategory { get; set; }


        [DisplayName("secondcategory name")]

        public int? SecondCategoryId { get; set; }
        [ValidateNever]
        public SecondCategoryDTO SecondCategory { get; set; }

 
        [DisplayName("thirdcategory name")]
        public int? ThirdCategoryId { get; set; }
        [ValidateNever]
        public ThirdCategoryDTO ThirdCategory { get; set; }

        [Required]
        [DisplayName("city name")]
        public int CityId { get; set; }
        [ValidateNever]
        public CityDTO City { get; set; }

        [DisplayName("state name")]
        [Required]
        public int StateId { get; set; }
        [ValidateNever]
        public StateDTO State { get; set; }

        [DisplayName("country name")]
        [Required]
        public int CountryId { get; set; }
        [ValidateNever]
        public CountryDTO Country { get; set; }

        [Required]
        public string Address { get; set; }
        [Required]
        public string Description { get; set; }

        
        [Required(ErrorMessage = "Phone number is required.")]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "Phone number should contain only numeric digits.")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "Phone number must be exactly 10 digits.")]
        public string PhoneNumber { get; set; }

        [StringLength(20)]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}", ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        public string? Certificate { get; set; }

        public bool? IsDelete { get; set; }

        public bool IsActive { get; set; }

        [StringLength(60, MinimumLength = 3, ErrorMessage = "enter  http://web.example.com. this type")]
        public string? WhatsApp { get; set; }

        [DisplayName("Instagram")]
        [RegularExpression(@"^(https?:\/\/)?([\da-z\.-]+)\.([a-z\.]{2,6})([\/\w \.-]*)*\/?$",
        ErrorMessage = "Enter this type url: https://web.exapmple.ema")]


        public string? InstagramId { get; set; }

        public string? Facebook { get; set; }

        public string? Website { get; set; }

        public string? Twitter  { get; set; }

        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

        [Required]
        [DisplayName("Year Of Establishment")]
        public DateTime EstablishDate { get; set; }


        //[ForeignKey("User")]
        //public int UserId { get; set; }
        //[ValidateNever]
        //public ApplicationUser ApplicationUser { get; set; }
    }
}
