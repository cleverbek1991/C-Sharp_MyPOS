using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyPOS.Models
{
    public class Order
    {
        [Key]
        public int OrderID { get; set; }

        // Paymen Type id is a nullable field 
        [Display(Name = "Payment Type")]
        public int? PaymentTypeID { get; set; }
        public PaymentType PaymentType { get; set; }

        [Required]
        [Display(Name = "Customer")]
        public int CustomerID { get; set; }
        public Customer Customer { get; set; }

        //Collection of orders in joint table 
        public ICollection<OrderMenu> OrderMenu { get; set; }
    }
}
