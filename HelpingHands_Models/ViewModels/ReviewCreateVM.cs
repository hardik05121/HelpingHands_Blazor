using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HelpingHands_Models.ViewModels
{
    public class ReviewCreateVM
    {
        public ReviewCreateVM()
        {
            Review = new ReviewCreateDTO();
            Company = new CompanyCreateDTO();
        }
        public ReviewCreateDTO Review { get; set; }
        public CompanyCreateDTO Company { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> CompanyList { get; set; }
    }
}
