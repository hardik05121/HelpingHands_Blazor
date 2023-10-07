namespace HelpingHands_Models.Index
{
    public class CountryIndexVM
    {
        public IEnumerable<CountryDTO> countries { get; set; } = new List<CountryDTO>();
        public string NameSortOrder { get; set; }
        public int PageSize { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public string Term { get; set; }
        public string OrderBy { get; set; }
    }
}
