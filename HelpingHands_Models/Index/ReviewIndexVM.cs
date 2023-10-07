namespace HelpingHands_Models.Index
{
    public class ReviewIndexVM
    {
        public IEnumerable<ReviewDTO> reviews { get; set; } = new List<ReviewDTO>();
        public string NameSortOrder { get; set; }
        // public string EmailSortOrder { get; set; }
        public int PageSize { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public string Term { get; set; }
        public string OrderBy { get; set; }
    }
}
