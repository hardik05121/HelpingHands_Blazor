
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HelpingHands_Models.ViewModels
{
    public class ReviewXCommentCreateVM
    {
        public ReviewXCommentCreateVM()
        {
            ReviewXComment = new ReviewXCommentCreateDTO();
        }
        public ReviewXCommentCreateDTO ReviewXComment { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> CompanyList { get; set; }
        [ValidateNever]
        public List<ReviewDTO> ReviewList{ get; set; }

	}
}
