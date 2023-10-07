namespace HelpingHands_Models.Index
{
    public class SecondCategoryIndexVM
    {
        public IEnumerable<SecondCategoryDTO> secondCategories { get; set; } = new List<SecondCategoryDTO>();
        public string NameSortOrder { get; set; }
        // public string EmailSortOrder { get; set; }
        public int PageSize { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public string Term { get; set; }
        public string OrderBy { get; set; }
    }
}
