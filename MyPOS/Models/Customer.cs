using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace MyPOS.Models
{
    public class Customer
    {
        [Key]
        public int CustomerID { get; set; }

        [Required]
        [Display(Name = "Name")]
        [StringLength(12, ErrorMessage = "Please shorten the Customer Name to 12 characters")]
        public string CustomerName { get; set; }

        [Required]
        [StringLength(50)]
        public string Address { get; set; }

        [Required(ErrorMessage = "Your must provide a Phone Number")]
        [Display(Name = "Phone Number")]
        [StringLength(12, ErrorMessage = "Please vali Phone Number")]
        public string Phone { get; set; }

        public virtual ICollection<Order> Orders { get; set; }

        public ICollection<OrderMenu> OrderMenu { get; set; }
    }
}
