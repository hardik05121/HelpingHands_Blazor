
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HelpingHands_Models.ViewModels
{
    public class SecondCategoryUpdateVM
    {
        public SecondCategoryUpdateVM()
        {
            SecondCategory = new SecondCategoryUpdateDTO();
        }

        public SecondCategoryUpdateDTO SecondCategory { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> FirstCategoryList { get; set; }
    }
}
