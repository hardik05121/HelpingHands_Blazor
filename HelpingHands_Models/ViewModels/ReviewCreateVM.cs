
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HelpingHands_Models.ViewModels
{
    public class ReviewCreateVM
    {
        public ReviewCreateVM()
        {
            Company = new CompanyCreateDTO();
            Review = new ReviewCreateDTO();
        }
        public ReviewCreateDTO Review { get; set; }
        public CompanyCreateDTO Company { get; set; }

        [ValidateNever]
        public IEnumerable<SelectListItem> CountryList { get; set; }


        [ValidateNever]
        public List<ReviewDTO> ReviewList { get; set; }
    }
}
