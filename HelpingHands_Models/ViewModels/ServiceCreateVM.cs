
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HelpingHands_Models.ViewModels
{
    public class ServiceCreateVM
    {
        public ServiceCreateVM()
        {
            Service = new ServiceCreateDTO();
        }
        public ServiceCreateDTO Service { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> FirstCategoryList { get; set; }

    }
}
