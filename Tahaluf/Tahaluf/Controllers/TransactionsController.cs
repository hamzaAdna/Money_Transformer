using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Tahaluf.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Tahaluf.Controllers
{
    public class TransactionsController : Controller
    {
        private readonly ModelContext _context;

        public TransactionsController(ModelContext context)
        {
            _context = context;
        }

        // GET: Transactions
        public async Task<IActionResult> Index()
        {
            var modelContext = _context.Transactions.Include(t => t.Wallet);
            return View(await modelContext.ToListAsync());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(DateTime? startdate, DateTime? enddate)
        {
            var item = _context.Transactions.Include(t => t.Wallet).ToListAsync();
         
            if(startdate !=null)
            {
                item = _context.Transactions.Include(t => t.Wallet).Where(x=> x.Transdate.Value.Date >= startdate.Value.Date).ToListAsync();
            }
            if(enddate !=null)
            {
                item = _context.Transactions.Include(t => t.Wallet).Where(x => x.Enddate.Value.Date <= enddate.Value.Date).ToListAsync();
            }
           

            //if ((startdate != null && enddate != null) || (startdate ==null && enddate !=null) || (startdate !=null && enddate==null ))
            //{
            //     item = _context.Transactions.Include(t => t.Wallet).Where(x => (x.Transdate != null && x.Enddate != null) && (x.Transdate.Value.Date >= startdate.Value.Date && x.Enddate.Value.Date <= enddate.Value.Date) ).ToListAsync();
            //    if()


            //}
            //else if (startdate == null && enddate ==null)
            //{

            //    item = _context.Transactions.Include(t => t.Wallet).ToListAsync();
            //}


            //      var modelContext = _context.Wallets.Include(w => w.Bank).Include(w => w.Useracount);
            return View(await item);



        }
        // GET: Transactions/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null || _context.Transactions == null)
            {
                return NotFound();
            }

            var transaction = await _context.Transactions
                .Include(t => t.Wallet)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (transaction == null)
            {
                return NotFound();
            }

            return View(transaction);
        }

        // GET: Transactions/Create
        public IActionResult Create()
        {
            ViewData["Walletid"] = new SelectList(_context.Wallets, "Id", "Id");
            return View();
        }

        // POST: Transactions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Transvlaue,Senderiban,Receiveriban,Commission,Transdate,Enddate,Walletid")] Transaction transaction)
        {
            if (ModelState.IsValid)
            {
                _context.Add(transaction);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Walletid"] = new SelectList(_context.Wallets, "Id", "Id", transaction.Walletid);
            return View(transaction);
        }

        // GET: Transactions/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null || _context.Transactions == null)
            {
                return NotFound();
            }

            var transaction = await _context.Transactions.FindAsync(id);
            if (transaction == null)
            {
                return NotFound();
            }
            ViewData["Walletid"] = new SelectList(_context.Wallets, "Id", "Id", transaction.Walletid);
            return View(transaction);
        }

        // POST: Transactions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Id,Transvlaue,Senderiban,Receiveriban,Commission,Transdate,Enddate,Walletid")] Transaction transaction)
        {
            if (id != transaction.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(transaction);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TransactionExists(transaction.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["Walletid"] = new SelectList(_context.Wallets, "Id", "Id", transaction.Walletid);
            return View(transaction);
        }

        // GET: Transactions/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null || _context.Transactions == null)
            {
                return NotFound();
            }

            var transaction = await _context.Transactions
                .Include(t => t.Wallet)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (transaction == null)
            {
                return NotFound();
            }

            return View(transaction);
        }

        // POST: Transactions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            if (_context.Transactions == null)
            {
                return Problem("Entity set 'ModelContext.Transactions'  is null.");
            }
            var transaction = await _context.Transactions.FindAsync(id);
            if (transaction != null)
            {
                _context.Transactions.Remove(transaction);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TransactionExists(decimal id)
        {
          return (_context.Transactions?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
