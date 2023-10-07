using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace HelpingHands_Models.ViewModels
{
    public class HomeVM
    {
        public HomeVM()
        {
            CompanyImage = new CompanyImageCreateDTO();
        }
        public CompanyImageCreateDTO CompanyImage { get; set; }

        public CompanyDTO Company { get; set; }



        [ValidateNever]
        public List<FirstCategoryDTO> FirstCategoryList { get; set; }

        [ValidateNever]
        public List<SecondCategoryDTO> SecondCategoryList { get; set; }

        [ValidateNever]
        public List<ThirdCategoryDTO> ThirdCategoryList { get; set; }

        [ValidateNever]
        public List<CompanyDTO> CompanyList { get; set; }
    }
}
