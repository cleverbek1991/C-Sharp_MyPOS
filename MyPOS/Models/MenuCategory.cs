using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyPOS.Models
{
    public class MenuCategory
    {
        [Key]
        public int MenuCategoryID { get; set; }

        [Required]
        [StringLength(30)]
        [Display(Name = "Category")]
        public string MCTitle { get; set; }

        public ICollection<MenuItem> MenuItem { get; set; }
    }
}
