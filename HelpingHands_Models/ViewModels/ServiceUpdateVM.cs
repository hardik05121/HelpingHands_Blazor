
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HelpingHands_Models.ViewModels
{
    public class ServiceUpdateVM
    {
        public ServiceUpdateVM()
        {
            Service = new ServiceUpdateDTO();
        }
        public ServiceUpdateDTO Service { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> FirstCategoryList { get; set; }

    }
}
