using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyPOS.Models
{
    public class PaymentType
    {
        [Key]
        public int PaymentTypeID { get; set; }

        [Required]
        [StringLength(12)]
        public string Type { get; set; }

        [Required]
        [StringLength(20)]
        public string AccountNumber { get; set; }

        public ICollection<Order> Orders { get; set; }
    }
}
