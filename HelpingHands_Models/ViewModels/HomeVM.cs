
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HelpingHands_Models.ViewModels
{
    public class HomeVM
    {
        public HomeVM()
        {
            CompanyImage = new CompanyImageCreateDTO();
            Enquiry = new EnquiryDTO();
            Company = new CompanyDTO();
            Review = new ReviewDTO();
        }
        public CompanyImageCreateDTO CompanyImage { get; set; }
        public CompanyDTO Company { get; set; }
        public ReviewDTO Review { get; set; }

        public ReviewXCommentCreateDTO ReviewXComment { get; set; } = new ReviewXCommentCreateDTO();
        public EnquiryDTO Enquiry { get; set; }
        public string ErrorMessage { get; set; }

        [ValidateNever]
        public List<ReviewDTO> ReviewList { get; set; }

        [ValidateNever]
        public List<FirstCategoryDTO> FirstCategoryList { get; set; }

        [ValidateNever]
        public List<SecondCategoryDTO> SecondCategoryList { get; set; }

        [ValidateNever]
        public List<ThirdCategoryDTO> ThirdCategoryList { get; set; }

        [ValidateNever]
        public List<CompanyDTO> CompanyList { get; set; }
        [ValidateNever]
        public List<CompanyImageDTO> CompanyImageList { get; set; }


        [ValidateNever]
        public List<CompanyXAmenityDTO> AmenityList { get; set; }
        [ValidateNever]
        public List<CompanyXPaymentDTO> PaymentList { get; set; }
        [ValidateNever]
        public List<CompanyXServiceDTO> ServiceList { get; set; }


        [ValidateNever]
        public List<ReviewXCommentDTO> ReviewXCommentList { get; set; }
    }
}







