using System.Collections.Generic;
using System.Linq;
using MyPOS.Data;

namespace MyPOS.Models.CustomerViewModels
{
    public class CustomerDetailViewModel
    {
        public Customer Customer { get; set; }
        public ICollection<MenuItem> MenuItem { get; set; }
        public ICollection<MenuCategory> MenuCategory { get; set; }
        public ICollection<Order> Order { get; set; }
        public ICollection<PaymentType> PaymentType { get; set; }
        public ICollection<OrderMenu> OrderMenu { get; set; }

        public IEnumerable<GroupedItems> GroupedItems { get; set; }

        public CustomerDetailViewModel(ApplicationDbContext context)
        {
            MenuItem = context.MenuItem.ToList();
            MenuCategory = context.MenuCategory.ToList();
            Order = context.Order.ToList();
            PaymentType = context.PaymentType.ToList();
            OrderMenu = context.OrderMenu.ToList();
        }
    }

    public class GroupedItems
    {
        public int CategoryID { get; set; }
        public string TypeName { get; set; }
        public IEnumerable<MenuItem> MenuItem { get; set; }
    }
}
