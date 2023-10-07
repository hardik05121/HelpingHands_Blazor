namespace HelpingHands_Models.Index
{
    public class ThirdCategoryIndexVM
    {
        public IEnumerable<ThirdCategoryDTO> thirdCategories { get; set; } = new List<ThirdCategoryDTO>();
        public string NameSortOrder { get; set; }
        // public string EmailSortOrder { get; set; }
        public int PageSize { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public string Term { get; set; }
        public string OrderBy { get; set; }
    }
}
