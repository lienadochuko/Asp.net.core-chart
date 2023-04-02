using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using study2.Data;
using study2.Models;

namespace study2.Controllers
{
       public class TransactionsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TransactionsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Transactions
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.transactions.Include(t => t.Category);
            return View(await applicationDbContext.ToListAsync());
        }



        // GET: Transactions/Create
        public async Task<IActionResult> AddOrEdit(int id = 0)
        {
                PopulateCategories();
            if (id == 0)
                return View(new Transactions());
            else
                return View(_context.transactions.Find(id));
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit([Bind("TransactionId,CategoryId,Amount,Note,Date")] Transactions transaction)
        {
            if (ModelState.IsValid)
            {
                    _context.Update(transaction);
                    await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            PopulateCategories();
            return View(transaction);
        }

        // POST: Transactions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.transactions == null)
            {
                return Problem("Entity set 'ApplicationDbContext.transactions'  is null.");
            }
            var transactions = await _context.transactions.FindAsync(id);
            if (transactions != null)
            {
                _context.transactions.Remove(transactions);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TransactionsExists(int id)
        {
          return (_context.transactions?.Any(e => e.TransactionsId == id)).GetValueOrDefault();
        }

        [NonAction]
        public void PopulateCategories()
        {
            var CategoryCollection = _context.Categories.ToList();
            Category DefaultCategory = new Category() { CategoryId = 0, Title = "Choose a category"};
            CategoryCollection.Insert(0, DefaultCategory);
            ViewBag.Categories = CategoryCollection;    
        }
       }
}
