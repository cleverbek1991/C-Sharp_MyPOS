using System.ComponentModel.DataAnnotations;

namespace MyPOS.Models
{
    public class OrderMenu
    {
        [Key]
        public int OrderMenuID { get; set; }

        [Required]
        public int MenuItemID { get; set; }
        public virtual MenuItem MenuItem { get; set; }

        [Required]
        public int OrderID { get; set; }
        public virtual Order Orders { get; set; }

        public int CustomerID { get; set; }
        public Customer Customer { get; set; }
    }
}
