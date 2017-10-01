using System;
using System.Collections.Generic;
using System.Linq;
using MyPOS.Data;

namespace MyPOS.Models.OrderViewModels
{
    public class OrderMenuViewModel
    {
        public OrderMenu OrderMenu { get; set; }

        public int OrderID { get; set; }
        public virtual Order Orders { get; set; }

        public int MenuItemID { get; set; }
        public virtual MenuItem MenuItem { get; set; }

        public int CustomerID { get; set; }
        public Customer Customer { get; set; }
        
    }
}
