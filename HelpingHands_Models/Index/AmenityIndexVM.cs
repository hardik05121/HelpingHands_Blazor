

namespace HelpingHands_Models.Index
{
    public class AmenityIndexVM
    {
        public IEnumerable<AmenityDTO> amenities { get; set; }  = new List<AmenityDTO>();
        public string NameSortOrder { get; set; }
        public int PageSize { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public string Term { get; set; }
        public string OrderBy { get; set; }
    }
}
