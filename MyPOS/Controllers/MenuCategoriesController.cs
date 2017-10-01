using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyPOS.Data;
using MyPOS.Models;

namespace MyPOS.Controllers
{
    public class MenuCategoriesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MenuCategoriesController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: MenuCategories
        public async Task<IActionResult> Index()
        {
            return View(await _context.MenuCategory.ToListAsync());
        }

        // GET: MenuCategories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menuCategory = await _context.MenuCategory
                .SingleOrDefaultAsync(m => m.MenuCategoryID == id);
            if (menuCategory == null)
            {
                return NotFound();
            }

            return View(menuCategory);
        }

        // GET: MenuCategories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MenuCategories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MenuCategoryID,MCTitle")] MenuCategory menuCategory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(menuCategory);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(menuCategory);
        }

        // GET: MenuCategories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menuCategory = await _context.MenuCategory.SingleOrDefaultAsync(m => m.MenuCategoryID == id);
            if (menuCategory == null)
            {
                return NotFound();
            }
            return View(menuCategory);
        }

        // POST: MenuCategories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MenuCategoryID,MCTitle")] MenuCategory menuCategory)
        {
            if (id != menuCategory.MenuCategoryID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(menuCategory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MenuCategoryExists(menuCategory.MenuCategoryID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(menuCategory);
        }

        // GET: MenuCategories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menuCategory = await _context.MenuCategory
                .SingleOrDefaultAsync(m => m.MenuCategoryID == id);
            if (menuCategory == null)
            {
                return NotFound();
            }

            return View(menuCategory);
        }

        // POST: MenuCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var menuCategory = await _context.MenuCategory.SingleOrDefaultAsync(m => m.MenuCategoryID == id);
            _context.MenuCategory.Remove(menuCategory);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool MenuCategoryExists(int id)
        {
            return _context.MenuCategory.Any(e => e.MenuCategoryID == id);
        }
    }
}
