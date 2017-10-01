using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyPOS.Models
{
    public class MenuItem
    {
        [Key]
        public int MenuItemID { get; set; }

        [Required]
        [Display(Name = "Menu Category")]
        public int MenuCategoryID { get; set; }

        public MenuCategory MenuCategory { get; set; }

        [Required]
        [StringLength(55, ErrorMessage = "Please shorten the Name to 55 characters")]
        public string MITitle { get; set; }

        [Required]
        [StringLength(55, ErrorMessage = "Please shorten the Description to 55 characters")]
        public string Description { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public double Price { get; set; }

        public virtual ICollection<OrderMenu> OrderMenu { get; set; }
    }
}
