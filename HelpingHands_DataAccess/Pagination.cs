using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HelpingHands_DataAccess
{
    public class Pagination
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
