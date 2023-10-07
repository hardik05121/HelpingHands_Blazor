namespace HelpingHands_Models.Index
{
    public class CompanyIndexVM
    {
        public IEnumerable<CompanyDTO> companies { get; set; } = new List<CompanyDTO>();
        public string NameSortOrder { get; set; }
        public int PageSize { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public string Term { get; set; }
        public string OrderBy { get; set; }
    }
}
