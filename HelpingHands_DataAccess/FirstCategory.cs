using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HelpingHands_DataAccess
{
    public class FirstCategory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [DisplayName("First Category")]
        public string FirstCategoryName { get; set; }

        [DisplayName("First CategoryImage")]
        public string FirstCategoryImage { get; set; }

        public bool IsActive { get; set; }


    }
}
