namespace HelpingHands_Models.Index
{
    public class ApplicationRoleIndexVM
    {
        public IEnumerable<ApplicationRoleDTO> applicationRoles { get; set; } = new List<ApplicationRoleDTO>();
        public string NameSortOrder { get; set; }
        public int PageSize { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public string Term { get; set; }
        public string OrderBy { get; set; }
    }
}
