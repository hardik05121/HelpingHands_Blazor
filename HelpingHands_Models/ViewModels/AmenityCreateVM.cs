
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HelpingHands_Models.ViewModels
{
    public class AmenityCreateVM
    {
        public AmenityCreateVM()
        {
            Amenity = new AmenityCreateDTO();
        }
        public AmenityCreateDTO Amenity { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> FirstCategoryList { get; set; }

    }
}
