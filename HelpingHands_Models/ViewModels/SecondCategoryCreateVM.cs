
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HelpingHands_Models.ViewModels
{
    public class SecondCategoryCreateVM
    {
        public SecondCategoryCreateVM()
        {
            SecondCategory = new SecondCategoryCreateDTO();
            
        }
        public SecondCategoryCreateDTO SecondCategory { get; set; }

    


        [ValidateNever]
        public IEnumerable<SelectListItem> FirstCategoryList { get; set; }
    }
}
