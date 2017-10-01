using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyPOS.Data;
using MyPOS.Models;
using MyPOS.Models.OrderViewModels;

namespace MyPOS.Controllers
{
    public class OrdersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OrdersController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: Orders
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Order.Include(o => o.Customer).Include(o => o.PaymentType);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Orders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Order
                .Include(o => o.Customer)
                .Include(o => o.PaymentType)
                .SingleOrDefaultAsync(m => m.OrderID == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // GET: Orders/Create
        public IActionResult Create()
        {
            ViewData["CustomerID"] = new SelectList(_context.Set<Customer>(), "CustomerID", "Address");
            ViewData["PaymentTypeID"] = new SelectList(_context.Set<PaymentType>(), "PaymentTypeID", "Type");
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrderID,PaymentTypeID,CustomerID")] Order order)
        {
            if (ModelState.IsValid)
            {
                _context.Add(order);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["CustomerID"] = new SelectList(_context.Set<Customer>(), "CustomerID", "Address", order.CustomerID);
            ViewData["PaymentTypeID"] = new SelectList(_context.Set<PaymentType>(), "PaymentTypeID", "Type", order.PaymentTypeID);
            return View(order);
        }

        public async Task<Order> CreateOrder(int id)
        {
            Order order = new Order();
            order.CustomerID = id;
            _context.Add(order);
            await _context.SaveChangesAsync();
            return order;
        }

        public async Task<IActionResult> AddProduct(int id, int custID)
        {
            CreateOrderViewModel model = new CreateOrderViewModel();

            model.Orders = await _context.Order.SingleOrDefaultAsync(m => m.PaymentTypeID == null);

            if (model.Orders == null)
            {
                model.Orders = await CreateOrder(custID);
            }

            OrderMenu orderMenu = new OrderMenu();
            orderMenu.CustomerID = custID;
            orderMenu.Orders = model.Orders;
            orderMenu.MenuItem = await _context.MenuItem.SingleOrDefaultAsync(p => p.MenuItemID == id);
            _context.Add(orderMenu);

            await _context.SaveChangesAsync();

            return RedirectToAction("Details", "Customers", new { id = orderMenu.CustomerID });
        }

        // GET: Orders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Order.SingleOrDefaultAsync(m => m.OrderID == id);
            if (order == null)
            {
                return NotFound();
            }
            ViewData["CustomerID"] = new SelectList(_context.Set<Customer>(), "CustomerID", "Address", order.CustomerID);
            ViewData["PaymentTypeID"] = new SelectList(_context.Set<PaymentType>(), "PaymentTypeID", "Type", order.PaymentTypeID);
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OrderID,PaymentTypeID,CustomerID")] Order order)
        {
            if (id != order.OrderID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(order);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.OrderID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Create", "Customers");
            }
            ViewData["CustomerID"] = new SelectList(_context.Set<Customer>(), "CustomerID", "Address", order.CustomerID);
            ViewData["PaymentTypeID"] = new SelectList(_context.Set<PaymentType>(), "PaymentTypeID", "Type", order.PaymentTypeID);
            return View(order);
        }

        // GET: Orders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Order
                .Include(o => o.Customer)
                .Include(o => o.PaymentType)
                .SingleOrDefaultAsync(m => m.OrderID == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var order = await _context.Order.SingleOrDefaultAsync(m => m.OrderID == id);
            _context.Order.Remove(order);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteProduct(int id)
        {

            var orderMenu = await _context.OrderMenu.SingleOrDefaultAsync(p => p.OrderMenuID == id);
            _context.OrderMenu.Remove(orderMenu);
            await _context.SaveChangesAsync();
            return RedirectToAction("Details", "Customers", new { id = orderMenu.CustomerID });
        }

        private bool OrderExists(int id)
        {
            return _context.Order.Any(e => e.OrderID == id);
        }
    }
}
