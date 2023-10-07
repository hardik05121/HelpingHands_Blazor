using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HelpingHands_DataAccess
{
    public class Company
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [DisplayName("Company Name")]
        public string CompanyName { get; set; }

        [ForeignKey("FirstCategory")]
        public int FirstCategoryId { get; set; }
        [ValidateNever]
        public FirstCategory FirstCategory { get; set; }

        [ForeignKey("SecondCategory")]
        public int? SecondCategoryId { get; set; }
        [ValidateNever]
        public SecondCategory SecondCategory { get; set; }

        [ForeignKey("ThirdCategory")]
        public int? ThirdCategoryId { get; set; }
        [ValidateNever]
        public ThirdCategory ThirdCategory { get; set; }

        [DisplayName("Company Logo")]
        public string CompanyLogo { get; set; }

        [ForeignKey("City")]
        public int CityId { get; set; }
        [ValidateNever]
        public City City { get; set; }

        [ForeignKey("State")]
        public int StateId { get; set; }
        [ValidateNever]
        public State State { get; set; }

        [ForeignKey("Country")]
        public int CountryId { get; set; }
        [ValidateNever]
        public Country Country { get; set; }

        [Required]
        public string Address { get; set; }
        [Required]
        public string Description { get; set; }

        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string Email { get; set; }

        public string? Certificate { get; set; }

        public bool? IsDelete { get; set; }

        public bool IsActive { get; set; }

        public string? WhatsApp { get; set; }

		[DisplayName("Instagram")]
		public string? InstagramId { get; set; }

        public string? Facebook { get; set; }

        public string? Website { get; set; }

        public string? Twitter  { get; set; }

        public DateTime? CreatedDate { get; set; } = System.DateTime.Now;
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
